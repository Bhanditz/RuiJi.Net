﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RuiJi.Net.Core.Compile;
using RuiJi.Net.Core.Extractor.Selector;

namespace RuiJi.Net.Core.Extractor.Processor
{
    /// <summary>
    /// external function processor
    /// </summary>
    public class FunctionProcessor : ProcessorBase<FunctionSelector>
    {
        /// <summary>
        /// process need
        /// </summary>
        /// <param name="selector">function selector</param>
        /// <param name="result">pre process result</param>
        /// <returns>new process result</returns>
        public override ProcessResult ProcessNeed(FunctionSelector selector, ProcessResult result)
        {
            var pr = new ProcessResult();

            var compile = new ProcessorCompile();
            var r = compile.GetResult(new KeyValuePair<string, string>(selector.Name, result.Content));

            if (r.Length > 0)
                pr.Matches.Add(r.First().ToString());
            else
                pr.Matches.Add(result.Content);

            return pr;
        }

        /// <summary>
        /// process remove,same as process need
        /// </summary>
        /// <param name="selector">function selector</param>
        /// <param name="result">pre process result</param>
        /// <returns>new process result</returns>
        public override ProcessResult ProcessRemove(FunctionSelector selector, ProcessResult result)
        {
            return ProcessNeed(selector, result);
        }
    }
}