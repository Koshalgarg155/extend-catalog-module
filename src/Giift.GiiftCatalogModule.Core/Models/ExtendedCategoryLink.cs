using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VirtoCommerce.CatalogModule.Core.Model;
using VirtoCommerce.Platform.Core.JsonConverters;

namespace Giift.GiiftCatalogModule.Core.Models
{
    public class ExtendedCategoryLink : CategoryLink
    {
        public bool IsItemRelationActive { get; set; } = true;
    }
}
