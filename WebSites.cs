//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CuratorsHelper
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebSites
    {
        public int id_site { get; set; }
        public Nullable<int> id_curator { get; set; }
        public string website { get; set; }
        public string url { get; set; }
    
        public virtual Curators Curators { get; set; }
    }
}