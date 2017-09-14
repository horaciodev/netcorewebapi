using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleAPI.Utils
{
    public class ValidationExtensions
    {

        public static int[] ParseProductIdsFromString(string productIds)
        {
            if(string.IsNullOrEmpty(productIds))
                return null;

            var listOfProductIds = new List<KeyValuePair<string, int>>();                

            var arrayOfProductsId = productIds.Split(',');
            int parseResult = 0;
            foreach(var str in arrayOfProductsId)
            {                
                if(int.TryParse(str,out parseResult))
                listOfProductIds.Add(new KeyValuePair<string, int>(str, parseResult));
            }

            var parsedListOfProductIds  = listOfProductIds
                                                .Where(x => x.Value > 0)
                                                .Select( x=> x.Value ).ToArray();

            return parsedListOfProductIds;                                                
        }
    }
}