//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUPD
//
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Gamification.Evaluations;
using Gamification.Extention;
using Gamification.Help;
using Gamification.View;
using UnityEngine;
using UnityEngine.Events;
using XdeEngine.Assembly;
using VMachina;

namespace Gamification
{
    public class GamificationController : MonoBehaviour
    {
        #region Attributes

        [Header("Scenarios")]
        [SerializeField]
        private XdeAsbScenario m_mainScenario = null;
        [SerializeField]
        protected List<XdeAsbScenario> m_optionalScenario = null;

        [Header("Controllers")]
        [SerializeField]
        private Evaluation m_evaluation = null;
        [SerializeField]
        private HelpController m_helpController = null;
        [SerializeField]
        private StartupScreen m_startupScreen = null;
        [SerializeField]
        private EndingScreen m_endingScreen = null;

        [Header("Events used by Haptic")]
        [SerializeField]
        private UnityEvent m_onEachXdeAsbStepComplete = null;
        [SerializeField]
        private UnityEvent m_onDangerousCollision = null;

        // Main scenario complete event maybe called many times
        private bool m_isEnded = false;

        #endregion

        #region Properties

        public XdeAsbScenario MainScenario
        {
            get => m_mainScenario;
            set => m_mainScenario = value;
        }

        public List<XdeAsbScenario> OptionalScenario
        {
            get => m_optionalScenario;
            set => m_optionalScenario = value;
        }

        public List<XdeAsbScenario> Scenarios
        {
            get
            {
                List<XdeAsbScenario> lScenarios = new List<XdeAsbScenario>();
                lScenarios.Add(MainScenario);

                if (OptionalScenario != null && OptionalScenario.Count > 0)
                    lScenarios.AddRange(OptionalScenario.FindAll(x => x != null));

                return lScenarios;
            }
        }

        public Evaluation Evaluation
        {
            get => m_evaluation;
            set => m_evaluation = value;
        }

        public HelpController HelpController
        {
            set => m_helpController = value;
        }

        public StartupScreen StartupScreen
        {
            get => m_startupScreen;
            set => m_startupScreen = value;
        }

        public EndingScreen EndingScreen
        {
            set => m_endingScreen = value;
        }

        public UnityEvent OnEachXdeAsbStepComplete => m_onEachXdeAsbStepComplete;
        public UnityEvent OnDangerousCollision => m_onDangerousCollision;

        #endregion

        #region Unity Callbacks

        private void OnEnable()
        {
            InitControllers();

            // Init listners
            MainScenario.completedEvent.AddListener(OnMainScenarioComplete);

            foreach (XdeAsbScenario l_scenario in Scenarios)
            {
                l_scenario.activationEvent.AddListener((XdeAsbStep s) => InitScenario(l_scenario));
            }
            foreach (CollisionTracker l_collision in Evaluation.GetStepsEvaluators<CollisionTracker>())
            {
                l_collision.OnCollideDangerousObject += () => OnDangerousCollision.Invoke();
            }
            Scenarios.Activate(false);
        }

        private void OnDisable()
        {
            // Remove listners
            MainScenario.completedEvent.RemoveListener(OnMainScenarioComplete);

            foreach (CollisionTracker l_collision in Evaluation.GetStepsEvaluators<CollisionTracker>())
            {
                l_collision.OnCollideDangerousObject -= () => OnDangerousCollision.Invoke();
            }
        }

        #endregion

        #region Logic

        public void InitControllers()
        {
            if (m_mainScenario == null)
                throw new ArgumentNullException(nameof(m_mainScenario));
            if (m_evaluation == null)
                throw new ArgumentNullException(nameof(m_evaluation));
            if (m_helpController == null)
                throw new ArgumentNullException(nameof(m_helpController));
            if (m_endingScreen == null)
                throw new ArgumentNullException(nameof(m_endingScreen));
            if (m_startupScreen == null)
                throw new ArgumentNullException(nameof(m_startupScreen));
            if (m_optionalScenario == null)
                m_optionalScenario = new List<XdeAsbScenario>();

            m_evaluation.Init(Scenarios);

            m_helpController.Init(Evaluation, MainScenario, OptionalScenario);
            m_helpController.DisplayGuidlinesAction = ConsumeVisualGuidesHint;
            m_helpController.NextStepAction = ConsumeNextStepHint;
            m_helpController.TodoListAction = ConsumeTodoListHint;

            Chronometer l_mainScenarioChrono = MainScenario.GetComponentInChildrenFDS<Chronometer>();
            m_endingScreen.Init();
            m_startupScreen.Init(m_evaluation.Metrics, l_mainScenarioChrono);
            m_startupScreen.StartScenarioAction = StartSimulation;

            m_isEnded = false;
        }

        private void InitScenario(XdeAsbScenario p_scenario)
        {
            foreach (XdeAsbStep l_step in p_scenario.steps)
            {
                l_step.completedEvent.AddListener(XdeAsbStepComplete);
            }
        }

        private void OnMainScenarioComplete(XdeAsbStep p_step)
        {
            if (m_isEnded)
                return;

            // Main scenario complete event maybe called many times
            m_isEnded = true;
            m_helpController.EnableHelps(false);

            // Scenario.compeleted event is called before its last step completed event
            // => wait end of frame to call scenario ended action to execute the step ended action before
            StartCoroutine(ScenarioEnd());
        }

        private IEnumerator ScenarioEnd()
        {
            yield return new WaitForEndOfFrame();

            m_evaluation.EndEvaluation();

            m_endingScreen.DisplayResultPanel(m_evaluation, m_evaluation.GetStepEvaluator<Chronometer>(MainScenario));
        }

        private void XdeAsbStepComplete(XdeAsbStep p_step)
        {
            if (!m_evaluation.HasBeenAbandoned(p_step))
            {
                if (OnEachXdeAsbStepComplete != null)
                {
                    OnEachXdeAsbStepComplete.Invoke();
                    p_step.completedEvent.RemoveListener(XdeAsbStepComplete);
                }
            }
        }

        private void StartSimulation()
        {
            m_helpController.EnableHelps(true);
            Scenarios.Activate(true);
        }

        private void ExecuteCmd(ICmd p_cmd)
        {
            p_cmd.Execute();
        }

        private async Task ExecuteCmd(IAsyncCmd p_cmd)
        {
            await p_cmd.Execute();
        }

        private void ConsumeTodoListHint()
        {
            ExecuteCmd(new TodoListDisplayCmd(this, m_helpController));
        }

        private void ConsumeVisualGuidesHint()
        {
            ExecuteCmd(new VisualGuidesCmd(this, m_helpController));
        }

        private async Task ConsumeNextStepHint()
        {
            await ExecuteCmd(new NextStepCmd(this, m_helpController));
        }

        #endregion
    }
}