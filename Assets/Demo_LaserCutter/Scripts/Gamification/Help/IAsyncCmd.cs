//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script developped by LS GROUP
//
//=============================================================================

using System.Threading.Tasks;

namespace Gamification.Help
{
    public interface IAsyncCmd
    {
        Task Execute();
    }
}