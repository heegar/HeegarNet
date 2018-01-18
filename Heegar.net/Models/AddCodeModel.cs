using Heegar.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heegar.net.Models
{
    public class AddCodeModel
    {
        public bool IsAuthenticated { get; set; }
        public int SelectedCodeType { get; set; }
        public List<CodeType> CodeTypes { get; set; }
        public string SelectedCodeSample { get; set; }
        public List<CodeSample> CodeSamplesForSelectedType { get; set; }

        public IEnumerable<SelectListItem> CodeTypesList
        {
            get
            {
                if (CodeTypes == null)
                    CodeTypes = new List<CodeType>();

                var codeListItems = CodeTypes.Select(emp => new SelectListItem { Value = emp.CodeTypeID.ToString(), Text = emp.Description });
                return DefaultCodeType.Concat(codeListItems);
            }
        }

        public IEnumerable<SelectListItem> DefaultCodeType
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "-1",
                    Text = "All"
                }, count: 1);
            }
        }
    }
}