namespace StaadPro.Interop.Enums.Root
{
    /// <summary>Contains the documented values for AnalysisMessageRetrievalStatus.</summary>
    public enum AnalysisMessageRetrievalStatus
    {
        /// <summary>Messages were retrieved. Value 1.</summary>
        Retrieved = 1,

        /// <summary>Successful retrieval found no messages. Value 0.</summary>
        NoMessages = 0,

        /// <summary>Native general error. Value -1.</summary>
        GeneralError = -1,

        /// <summary>Native invalid model path. Value -2.</summary>
        InvalidModelPath = -2,

        /// <summary>Native output argument is not an array. Value -107.</summary>
        NotAnArray = -107,

        /// <summary>Native output argument is not a BSTR string array. Value -119.</summary>
        InvalidArrayType = -119,

        /// <summary>Native output capacity is insufficient. Value -108.</summary>
        InsufficientCapacity = -108,

        /// <summary>The required log or ANL source is missing. Value -1001.</summary>
        SourceFileMissing = -1001,

        /// <summary>Analysis is still running. Value -10004.</summary>
        AnalysisRunning = -10004
    }
}
