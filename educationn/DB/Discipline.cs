//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace educationn.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Discipline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discipline()
        {
            this.Application = new HashSet<Application>();
            this.Exzamen = new HashSet<Exzamen>();
        }
    
        public int Code_dic { get; set; }
        public Nullable<int> Volume { get; set; }
        public string Name { get; set; }
        public string Executor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Application { get; set; }
        public virtual Cafedra Cafedra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exzamen> Exzamen { get; set; }
    }
}
