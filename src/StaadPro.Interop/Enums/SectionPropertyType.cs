namespace StaadPro.Interop.Enums
{
    /// <summary>Preserves the main-project native section-type codes; tube shape selectors 1-6 are not top-level section layouts.</summary>
    public enum SectionPropertyType
    {
        /// <summary>Native BeamST code 610; interpretation uses the acquired STAAD layout profile.</summary>
        BeamST = 610,
        /// <summary>Native BeamD code 616; interpretation uses the acquired STAAD layout profile.</summary>
        BeamD = 616,
        /// <summary>Native BeamTC code 613; interpretation uses the acquired STAAD layout profile.</summary>
        BeamTC = 613,
        /// <summary>Native BeamBC code 614; interpretation uses the acquired STAAD layout profile.</summary>
        BeamBC = 614,
        /// <summary>Native BeamTB code 615; interpretation uses the acquired STAAD layout profile.</summary>
        BeamTB = 615,
        /// <summary>Native BeamT code 611; interpretation uses the acquired STAAD layout profile.</summary>
        BeamT = 611,
        /// <summary>Native BeamCM code 612; interpretation uses the acquired STAAD layout profile.</summary>
        BeamCM = 612,
        /// <summary>Native ChannelST code 630; interpretation uses the acquired STAAD layout profile.</summary>
        ChannelST = 630,
        /// <summary>Native ChannelD code 631; interpretation uses the acquired STAAD layout profile.</summary>
        ChannelD = 631,
        /// <summary>Native ChannelFR code 633; interpretation uses the acquired STAAD layout profile.</summary>
        ChannelFR = 633,
        /// <summary>Native AngleST code 640; interpretation uses the acquired STAAD layout profile.</summary>
        AngleST = 640,
        /// <summary>Native AngleLD code 642; interpretation uses the acquired STAAD layout profile.</summary>
        AngleLD = 642,
        /// <summary>Native AngleSD code 643; interpretation uses the acquired STAAD layout profile.</summary>
        AngleSD = 643,
        /// <summary>Native AngleRA code 641; interpretation uses the acquired STAAD layout profile.</summary>
        AngleRA = 641,
        /// <summary>Native AngleSA code 646; interpretation uses the acquired STAAD layout profile.</summary>
        AngleSA = 646,
        /// <summary>Native PipeST code 660; interpretation uses the acquired STAAD layout profile.</summary>
        PipeST = 660,
        /// <summary>Native RectangleHSS code 654; interpretation uses the acquired STAAD layout profile.</summary>
        RectangleHSS = 654,
        /// <summary>Native RoundHSS code 655; interpretation uses the acquired STAAD layout profile.</summary>
        RoundHSS = 655,
        /// <summary>Native CastellatedST code 656; interpretation uses the acquired STAAD layout profile.</summary>
        CastellatedST = 656,
        /// <summary>Native TubeST code 650; interpretation uses the acquired STAAD layout profile.</summary>
        TubeST = 650,
        /// <summary>Native TeeST code 620; interpretation uses the acquired STAAD layout profile.</summary>
        TeeST = 620,
        /// <summary>Native PlateStrip code 666; interpretation uses the acquired STAAD layout profile.</summary>
        PlateStrip = 666,
        /// <summary>Native AngleColdST code 644; interpretation uses the acquired STAAD layout profile.</summary>
        AngleColdST = 644,
        /// <summary>Native AngleColdWithLipsST code 645; interpretation uses the acquired STAAD layout profile.</summary>
        AngleColdWithLipsST = 645,
        /// <summary>Native ChannelColdST code 634; interpretation uses the acquired STAAD layout profile.</summary>
        ChannelColdST = 634,
        /// <summary>Native ChannelColdWithLipsST code 635; interpretation uses the acquired STAAD layout profile.</summary>
        ChannelColdWithLipsST = 635,
        /// <summary>Native ZeesColdST code 662; interpretation uses the acquired STAAD layout profile.</summary>
        ZeesColdST = 662,
        /// <summary>Native ZeesColdWithLips code 663; interpretation uses the acquired STAAD layout profile.</summary>
        ZeesColdWithLips = 663,
        /// <summary>Native HatColdST code 664; interpretation uses the acquired STAAD layout profile.</summary>
        HatColdST = 664,
        /// <summary>Native TaperI code 680; interpretation uses the acquired STAAD layout profile.</summary>
        TaperI = 680,
        /// <summary>Native TaperedTube code 675; interpretation uses the acquired STAAD layout profile.</summary>
        TaperedTube = 675,
        /// <summary>Native PrismaticCircle code 671; interpretation uses the acquired STAAD layout profile.</summary>
        PrismaticCircle = 671,
        /// <summary>Native PrismaticRectangle code 672; interpretation uses the acquired STAAD layout profile.</summary>
        PrismaticRectangle = 672,
        /// <summary>Native PrismaticTrapezoidal code 674; interpretation uses the acquired STAAD layout profile.</summary>
        PrismaticTrapezoidal = 674,
        /// <summary>Native PrismaticTee code 673; interpretation uses the acquired STAAD layout profile.</summary>
        PrismaticTee = 673,
        /// <summary>Native PrismaticGeneral code 676; interpretation uses the acquired STAAD layout profile.</summary>
        PrismaticGeneral = 676,
        /// <summary>Native SolidRound code 668; interpretation uses the acquired STAAD layout profile.</summary>
        SolidRound = 668,
        /// <summary>Native PrismaticUPT code 699; interpretation uses the acquired STAAD layout profile.</summary>
        PrismaticUPT = 699,
        /// <summary>Native GeneralUPT code 697; interpretation uses the acquired STAAD layout profile.</summary>
        GeneralUPT = 697,
        /// <summary>Native WideFlangeUPT code 690; interpretation uses the acquired STAAD layout profile.</summary>
        WideFlangeUPT = 690,
        /// <summary>Native ChannelUPT code 691; interpretation uses the acquired STAAD layout profile.</summary>
        ChannelUPT = 691,
        /// <summary>Native AngleUPT code 692; interpretation uses the acquired STAAD layout profile.</summary>
        AngleUPT = 692,
        /// <summary>Native DoubleAngleUPT code 693; interpretation uses the acquired STAAD layout profile.</summary>
        DoubleAngleUPT = 693,
        /// <summary>Native TeeUPT code 694; interpretation uses the acquired STAAD layout profile.</summary>
        TeeUPT = 694,
        /// <summary>Native PipeUPT code 695; interpretation uses the acquired STAAD layout profile.</summary>
        PipeUPT = 695,
        /// <summary>Native TubeUPT code 696; interpretation uses the acquired STAAD layout profile.</summary>
        TubeUPT = 696,
        /// <summary>Native ISectionUPT code 698; interpretation uses the acquired STAAD layout profile.</summary>
        ISectionUPT = 698,
        /// <summary>Native Round code 1; interpretation uses the acquired STAAD layout profile.</summary>
        Round = 1,
        /// <summary>Native Hexdecagonal code 2; interpretation uses the acquired STAAD layout profile.</summary>
        Hexdecagonal = 2,
        /// <summary>Native Dodecagonal code 3; interpretation uses the acquired STAAD layout profile.</summary>
        Dodecagonal = 3,
        /// <summary>Native Octagonal code 4; interpretation uses the acquired STAAD layout profile.</summary>
        Octagonal = 4,
        /// <summary>Native Hexagonal code 5; interpretation uses the acquired STAAD layout profile.</summary>
        Hexagonal = 5,
        /// <summary>Native Square code 6; interpretation uses the acquired STAAD layout profile.</summary>
        Square = 6,
    }
}
