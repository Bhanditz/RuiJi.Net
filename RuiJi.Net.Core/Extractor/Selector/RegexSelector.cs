﻿using Newtonsoft.Json;
using RuiJi.Net.Core.Extractor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Net.Core.Extractor.Selector
{
    /// <summary>
    /// regex selector
    /// </summary>
    public class RegexSelector : SelectorBase
    {
        /// <summary>
        /// regex pattern
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// match group index array
        /// </summary>
        [JsonProperty("index")]
        public int[] Index { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="pattern">regex pattern</param>
        /// <param name="index">match group index</param>
        /// <param name="remove">remove flag</param>
        public RegexSelector(string pattern, int index = 0, bool remove = false)
        {
            this.Index = new int[] { index };
            this.Pattern = pattern;
            this.Remove = remove;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="pattern">regex pattern</param>
        /// <param name="index">match group index array</param>
        /// <param name="remove">remove flag</param>
        public RegexSelector(string pattern, int[] index, bool remove = true)
        {
            this.Index = index;
            this.Pattern = pattern;
            this.Remove = remove;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public RegexSelector()
        { }

        /// <summary>
        /// set selector type enum
        /// </summary>
        /// <returns>selector type</returns>
        protected override SelectorTypeEnum SetSelectType()
        {
            return SelectorTypeEnum.REGEX;
        }
    }
}
