using System.Collections.Generic;
using System.Linq;

namespace RemotePlanning.Ui.StorycardsUi
{
    public class BatchStorycardTextParser
    {
        public List<StorycardViewModel> Parse(string text)
        {
            return text.Split('\n')
                .Select(ParseOneStorycard)
                .ToList();
        }

        private StorycardViewModel ParseOneStorycard(string arg)
        {
            var storycardData = arg.Split('\t');
            return new StorycardViewModel
            {
                Number = GetString(storycardData, 0),
                Title = GetString(storycardData, 1),
                Estimate = GetNumber(storycardData, 2)
            };
        }

        private int GetNumber(string[] storycardData, int index)
        {
            int estimate = 0;
            int.TryParse(GetString(storycardData, index), out estimate);
            return estimate;
        }

        private string GetString(string[] storycardData, int i)
        {
            if (storycardData.Length > i)
            {
                return storycardData[i];
            }
            return string.Empty;
        }
    }
}