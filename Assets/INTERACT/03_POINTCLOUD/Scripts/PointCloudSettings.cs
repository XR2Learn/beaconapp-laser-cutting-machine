namespace INTERACT.Editor.PointCloud
{
    public enum PclType
    {
        INCORE = 0,
        OUTOFCORE = 1
    };

    // ReSharper disable once InconsistentNaming
    public class PointCloudSettings
    {

        public string m_filePath;
        public PclType m_type;
        public readonly ulong m_pointBudget = 4000000;
        public readonly ulong m_pointBudgetMem = 8000000;


    }
}
