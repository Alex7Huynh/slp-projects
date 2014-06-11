using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace MTT
{
    public class FileManager
    {
        // Properties
        public string MfcTrainFolder = "MfcTrain";
        public string MfcTestFolder = "MfcTest";
        public string LanguageModelFolder = "LM";
        public string HMMFolder = "HMM";

        public string DictFilename = "DICT";
        public string MonophonesFilename = "monophones";
        public string FulllistFilename = "fulllist";
        public string PromptsFilename = "PROMPTS";
        public string WordsFilename = "WORDS.MLF";
        public string MfccTrainScpFilename = "mfcc-train.scp";
        public string TrainScpFilename = "train.scp";

        public string MfccTestScpFilename = "mfcc-test.scp";
        public string TestScpFilename = "test.scp";
        public string TestMlfFilename = "test.mlf";

        public string LmTrainFilename = "lmtrain.txt";
        public string LmTestFilename = "lmtest.txt";

        private string TrainFilePath;
        private string TestFilePath;
        //Constructor
        public FileManager() { }
        public FileManager(string pTrainFilePath, string pTestFilePath)
        {
            TrainFilePath = pTrainFilePath;
            TestFilePath = pTestFilePath;
        }
        // Methods
        public bool CreateFolders()
        {
            string TmpTrainFilePath = TrainFilePath.Substring(0, TrainFilePath.LastIndexOf('\\'));

            Directory.CreateDirectory(TmpTrainFilePath + "\\" + MfcTrainFolder);
            Directory.CreateDirectory(TmpTrainFilePath + "\\" + MfcTestFolder);
            Directory.CreateDirectory(TmpTrainFilePath + "\\" + LanguageModelFolder);

            for (int i = 0; i <= 17; ++i)
            {
                Directory.CreateDirectory(TmpTrainFilePath + "\\" + HMMFolder + i);
            }

            return true;
        }
        public bool MakeDict(bool bSilence)
        {
            string[] files = Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;

            if (TrainFileCount == 0)
                return false;

            var lstWord = new List<string>();

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string[] lines = File.ReadAllLines(files[i]);
                string sentence = lines[0];
                string[] words = sentence.Split(' ');
                for (int j = 0; j < words.Length; ++j)
                {
                    string word1 = WordConversion.ConvertUnicodeToTelex(words[j]);
                    string word2 = WordConversion.ConvertUnicodeToPhone(words[j]);
                    word2 = WordConversion.NormalizePhone(word2);
                    string word = word1 + "    " + word2 + " sp";

                    lstWord.Add(word);
                }
            }
            lstWord = lstWord.Distinct().ToList();
            // MINH add code here for add new line
            lstWord.Sort();
            var fDICT = new StreamWriter(DictFilename);
            for (int i = 0; i < lstWord.Count; ++i)
            {
                fDICT.WriteLine(lstWord[i]);
            }
            fDICT.WriteLine("SENT-START	[]	sil");
            fDICT.WriteLine("SENT-END	[]	sil");
            if(bSilence)
                fDICT.WriteLine("silence [] sil");

            fDICT.Close();
            return true;
        }
        public bool MakeMonophones()
        {
            // Read Dict file
            string[] lines = File.ReadAllLines(DictFilename);

            if (lines.Length == 0)
                return false;

            // Get all monophones to list
            var lstMonophone = new List<string>();
            foreach (string line in lines)
            {
                string[] words = Regex.Split(line, " ");

                for (int i = 1; i < words.Length - 1; ++i)
                {
                    if (words[i].Contains(" ") || words[i] == "")
                        continue;
                    lstMonophone.Add(words[i]);
                }
            }

            lstMonophone = lstMonophone.Distinct().ToList();
            lstMonophone.Sort();

            // Write to files
            var fMonophone0 = new StreamWriter(MonophonesFilename + "0");
            var fMonophone1 = new StreamWriter(MonophonesFilename + "1");

            foreach (string word in lstMonophone)
            {
                fMonophone0.WriteLine(word);
                fMonophone1.WriteLine(word);
            }
            fMonophone0.WriteLine("sil");
            fMonophone1.WriteLine("sil");
            fMonophone1.WriteLine("sp");

            fMonophone0.Close();
            fMonophone1.Close();

            return true;
        }
        public bool MakeFulllist()
        {
            string[] monophones = File.ReadAllLines(MonophonesFilename + "1");

            var fFulllist = new StreamWriter(FulllistFilename);
            int n = monophones.Length;

            string tmp = monophones[n - 2];
            monophones[n - 2] = monophones[n - 1];
            monophones[n - 1] = tmp;

            // Monophone
            fFulllist.WriteLine("sil");
            fFulllist.WriteLine("sp");

            // Triphone
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n - 1; ++j)
                    for (int k = 0; k < n; ++k)
                        fFulllist.WriteLine("{0}-{1}+{2}", monophones[i], monophones[j], monophones[k]);

            fFulllist.Close();
            return true;
        }
        public bool MakePrompts()
        {
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);
            string[] files = Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;

            if (TrainFileCount == 0)
                return false;

            var fPROMPTS = new StreamWriter(PromptsFilename);
            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Remove(0, 1);

                string[] lines = File.ReadAllLines(files[i]);
                string newSentence = "";
                string sentence = lines[0];
                string[] words = sentence.Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                {
                    string word = WordConversion.ConvertUnicodeToTelex(words[j]);
                    newSentence += word + " ";
                }
                newSentence += WordConversion.ConvertUnicodeToTelex(words[words.Length - 1]);
                fPROMPTS.WriteLine(lastFolder + "\\" + filename + "  " + newSentence);
            }
            fPROMPTS.Close();
            return true;
        }
        public bool MakeWords()
        {
            string[] files = Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;

            if (TrainFileCount == 0)
                return false;

            var fWORDS = new StreamWriter(WordsFilename);
            fWORDS.WriteLine("#!MLF!#");

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Remove(0, 1);
                filename = filename.Replace("txt", "lab");

                fWORDS.WriteLine("\"*/" + filename + "\"");

                string[] lines = File.ReadAllLines(files[i]);
                string sentence = lines[0];
                string[] words = sentence.Split(' ');
                for (int j = 0; j < words.Length; ++j)
                {
                    string word = WordConversion.ConvertUnicodeToTelex(words[j]);
                    fWORDS.WriteLine(word);
                }
                fWORDS.WriteLine(".");
            }
            fWORDS.Close();
            return true;
        }
        public bool MakeMfccTrainScp()
        {
            var fMFC = new StreamWriter(MfccTrainScpFilename);
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(TrainFilePath, "*.wav");
            int TrainFileCount = files.Length;

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                string filename2 = filename.Replace("wav", "mfc");
                fMFC.WriteLine(lastFolder + filename + "  " + MfcTrainFolder + filename2);
            }
            fMFC.Close();
            return true;
        }
        public bool MakeTrainScp()
        {
            var fTraining = new StreamWriter(TrainScpFilename);
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(TrainFilePath, "*.wav");
            int TrainFileCount = files.Length;

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Replace("wav", "mfc");
                fTraining.WriteLine(MfcTrainFolder + filename);
            }
            fTraining.Close();
            return true;
        }

        public bool MakeMacrosHMM0()
        {
            string[] proto = File.ReadAllLines("HMM0\\proto");
            string[] vFloors = File.ReadAllLines("HMM0\\vFloors");
            var fMacros = new StreamWriter("HMM0\\macros");
            for (int i = 0; i < 3; ++i)
            {
                fMacros.WriteLine(proto[i]);
            }
            for (int i = 0; i < vFloors.Length; ++i)
            {
                fMacros.WriteLine(vFloors[i]);
            }
            fMacros.Close();
            return true;
        }
        public bool MakeHmmdefs()
        {
            var fHmmdefs = new StreamWriter("HMM0\\hmmdefs");
            string[] proto = File.ReadAllLines("HMM0\\proto");
            string[] phones = File.ReadAllLines("monophones0");
            for (int i = 0; i < phones.Length; ++i)
            {
                fHmmdefs.WriteLine("~h \"" + phones[i] + "\"");
                for (int j = 4; j < proto.Length; ++j)
                {
                    fHmmdefs.WriteLine(proto[j]);
                }
            }
            fHmmdefs.Close();
            return true;
        }
        public bool CreateHMM4()
        {
            string str;
            int num3;
            File.Copy("HMM3/hmmdefs", "HMM4/hmmdefs", true);
            File.Copy("HMM3/macros", "HMM4/macros", true);
            using (var reader = new StreamReader("HMM4/hmmdefs"))
            {
                str = reader.ReadToEnd();
            }
            int index = str.IndexOf("sil");
            string str2 = str.Substring(index - 4);
            index = str2.IndexOf("<STATE> 3");
            int num2 = str2.IndexOf("<STATE>", index + 0x16);
            string str3 = str2.Substring(index + 0x15, num2 - (index + 0x15));
            var writer = new StreamWriter("HMM4/hmmdefs");
            writer.Write(str);
            writer.WriteLine("~h \"sp\"");
            writer.WriteLine("<BEGINHMM>");
            writer.WriteLine("<NUMSTATES> 3");
            writer.WriteLine("<STATE> 2");
            writer.WriteLine("<MEAN> 39");
            writer.Write(str3);
            writer.WriteLine("<TRANSP> 3");
            index = str2.IndexOf("<TRANSP> 5");
            num2 = str2.IndexOf("<ENDHMM>");
            string[] strArray = str2.Substring(index + 11, num2 - (index + 11)).Split(new[] { '\n' });
            string[] strArray2 = strArray[0].Split(new[] { ' ' });
            string str4 = " ";
            for (num3 = 1; num3 <= 3; num3++)
            {
                str4 = str4 + strArray2[num3] + " ";
            }
            str4 = str4.Substring(0, str4.Length - 1);
            writer.WriteLine(str4);
            strArray2 = strArray[2].Split(new[] { ' ' });
            str4 = " " + strArray2[1] + " " + strArray2[3] + " " + strArray2[4];
            writer.WriteLine(str4);
            strArray2 = strArray[4].Split(new[] { ' ' });
            str4 = " ";
            for (num3 = 1; num3 <= 3; num3++)
            {
                str4 = str4 + strArray2[num3] + " ";
            }
            writer.WriteLine(str4);
            writer.WriteLine("<ENDHMM>");
            writer.Close();
            return true;
        }
        public bool ModifyWintriMlf()
        {
            string str;
            using (var reader = new StreamReader("wintri.mlf"))
            {
                str = reader.ReadToEnd();
            }

            string[] strArray = str.Split(new[] { '\n' });
            var writer = new StreamWriter("wintri.mlf");
            foreach (string strTmp in strArray)
            {
                string tmp = strTmp.Trim();
                if (strTmp.Contains(".lab\""))
                {
                    tmp = "\"*/" + tmp.Substring(1);
                }
                writer.WriteLine(tmp);
            }

            writer.Close();
            return true;
        }

        public bool MakeMfccTestScp()
        {
            var fMFC = new StreamWriter(MfccTestScpFilename);
            string lastFolder = TestFilePath.Substring(TestFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(TestFilePath, "*.wav");

            foreach (string file in files)
            {
                string filename = file.Replace(TestFilePath, "");
                string filename2 = filename.Replace("wav", "mfc");
                fMFC.WriteLine(lastFolder + filename + "  " + MfcTestFolder + filename2);
            }
            fMFC.Close();
            return true;
        }
        public bool MakeTestScp()
        {
            var fTest = new StreamWriter(TestScpFilename);
            string lastFolder = TestFilePath.Substring(TestFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(TestFilePath, "*.wav");
            int TrainFileCount = files.Length;

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TestFilePath, "");
                filename = filename.Replace("wav", "mfc");
                fTest.WriteLine(MfcTestFolder + filename);
            }
            fTest.Close();
            return true;
        }
        public bool MakeTestMlf()
        {
            var fTest = new StreamWriter(TestMlfFilename);
            fTest.WriteLine("#!MLF!#");

            string[] filesTest = Directory.GetFiles(TestFilePath, "*.txt");

            for (int i = 0; i < filesTest.Length; ++i)
            {
                string filename = filesTest[i].Replace(TestFilePath, "");
                filename = filename.Replace(".txt", ".lab");
                fTest.WriteLine("\"*" + filename.Replace('\\', '/') + "\"");

                string[] lines = File.ReadAllLines(filesTest[i]);
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length; ++j)
                    fTest.WriteLine(WordConversion.ConvertUnicodeToTelex(words[j]));

                fTest.WriteLine(".");
            }
            fTest.Close();
            return true;
        }
        public bool MakeLmTrain()
        {
            string[] filesTrain = Directory.GetFiles(TrainFilePath, "*.txt");
            string[] filesTest = Directory.GetFiles(TestFilePath, "*.txt");

            var fLmTrain = new StreamWriter(LmTrainFilename);
            // 270 lines
            for (int i = 0; i < filesTrain.Length; ++i)
            {
                string[] lines = File.ReadAllLines(filesTrain[i]);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += WordConversion.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += WordConversion.ConvertUnicodeToTelex(words[words.Length - 1]);
                fLmTrain.WriteLine("<s> " + newSentence + " </s>");
            }
            // 30 lines
            for (int i = 0; i < filesTest.Length; ++i)
            {
                string[] lines = File.ReadAllLines(filesTest[i]);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += WordConversion.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += WordConversion.ConvertUnicodeToTelex(words[words.Length - 1]);
                fLmTrain.WriteLine("<s> " + newSentence + " </s>");
            }

            fLmTrain.Close();
            return true;
        }
        public bool MakeLmTrain(string NewSentencesFile)
        {
            string[] filesTrain = Directory.GetFiles(TrainFilePath, "*.txt");

            var fLmtrain = new StreamWriter(LmTrainFilename);
            // 270 lines
            for (int i = 0; i < filesTrain.Length; ++i)
            {
                string[] lines = File.ReadAllLines(filesTrain[i]);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += WordConversion.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += WordConversion.ConvertUnicodeToTelex(words[words.Length - 1]);
                fLmtrain.WriteLine("<s> " + newSentence + " </s>");
            }
            // n lines            
            if (File.Exists(NewSentencesFile))
            {
                string[] newlines = File.ReadAllLines(NewSentencesFile);
                for (int i = 0; i < newlines.Length; ++i)
                {
                    newlines[i] = newlines[i].Trim();
                    if (newlines.Length != 0)
                    {
                        string newSentence = "";
                        string[] words = newlines[i].Split(' ');
                        for (int j = 0; j < words.Length - 1; ++j)
                            newSentence += WordConversion.ConvertUnicodeToTelex(words[j]) + " ";
                        newSentence += WordConversion.ConvertUnicodeToTelex(words[words.Length - 1]);
                        fLmtrain.WriteLine("<s> " + newSentence + " </s>");
                    }
                }
            }

            fLmtrain.Close();
            return true;
        }
        public bool MakeLmTest()
        {
            var fLmTestFile = new StreamWriter(LmTestFilename);
            string[] filesTest = Directory.GetFiles(TestFilePath, "*.txt");
            foreach (var fileTest in filesTest)
            {
                string[] lines = File.ReadAllLines(fileTest);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += WordConversion.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += WordConversion.ConvertUnicodeToTelex(words[words.Length - 1]);
                fLmTestFile.WriteLine("<s> " + newSentence + " </s>");
            }
            fLmTestFile.Close();
            return true;
        }
    }
}
