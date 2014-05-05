using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    public static class ConstantValues
    {

        public const string CMD_STEP0_CREATE_PHONE0 = "HLEd -l * -d dict -i phones0.mlf config\\mkphone0.led words.mlf";
        public const string CMD_STEP0_CREATE_PHONE1 = "HLEd -l * -d dict -i phones1.mlf config\\mkphone1.led words.mlf";
        public const string CMD_STEP0_CREATE_MFC = "HCopy -T 1 -C config\\config.hcopy -S mfcc.scp";
        public const string CMD_STEP7_HMM0_CMD = "HCompV -C config\\config -f 0.01 -m -S train.scp -M hmm0 config\\proto";

        public const string CMD_STEP9_HMM1_TRAIN =
            "HERest -C config\\config -I phones0.mlf -S train.scp -H hmm0/macros -H hmm0/hmmdefs -M hmm1 config\\monophones0";
        public const string CMD_STEP9_HMM2_TRAIN =
            "HERest -C config\\config -I phones0.mlf -S train.scp -H hmm0/macros -H hmm1/hmmdefs -M hmm2 config\\monophones0";
        public const string CMD_STEP9_HMM3_TRAIN =
            "HERest -C config\\config -I phones0.mlf -S train.scp -H hmm0/macros -H hmm2/hmmdefs -M hmm3 config\\monophones0";

        public const string CMD_STEP11_CONNECTSILSP = "HHEd -H hmm4/macros -H hmm4/hmmdefs -M hmm5 config\\sil.hed config\\monophones1";

        public const string CMD_STEP12_TRAINTOHMM6 =
            "HERest -C config\\config -I phones0.mlf -S train.scp -H hmm5/macros -H hmm5/hmmdefs -M hmm6 config\\monophones1";
        public const string CMD_STEP12_TRAINTOHMM7 =
           "HERest -C config\\config -I phones0.mlf -S train.scp -H hmm6/macros -H hmm6/hmmdefs -M hmm7 config\\monophones1";

        public const string CMD_STEP13_TRAINTOHMM8 =
            "HERest -C config\\config -I aligned.mlf -S train.scp -H hmm7/macros -H hmm7/hmmdefs -M hmm8 config\\monophones1";
        public const string CMD_STEP13_TRAINTOHMM9 =
           "HERest -C config\\config -I aligned.mlf -S train.scp -H hmm8/macros -H hmm8/hmmdefs -M hmm9 config\\monophones1";

        public const string CMD_STEP14_CREATEWINTRI = "HLEd -n triphones1 -i wintri.mlf config\\mktri.led aligned.mlf";

        public const string CMD_STEP14_TRAINTOHMM10 =
            "HHEd -B -H hmm9/macros -H hmm9/hmmdefs -M hmm10 config\\mktri.hed monophones1";
    }
}
