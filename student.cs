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
    
    public partial class student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public student()
        {
            this.Brothers_sisters = new HashSet<Brothers_sisters>();
            this.Hostel = new HashSet<Hostel>();
            this.Parents = new HashSet<Parents>();
            this.Passport = new HashSet<Passport>();
            this.Works_parent = new HashSet<Works_parent>();
            this.Works_student = new HashSet<Works_student>();
            this.Achivment_student = new HashSet<Achivment_student>();
            this.Facts_assotial = new HashSet<Facts_assotial>();
            this.Student_Hobbies = new HashSet<Student_Hobbies>();
        }
    
        public int id_student { get; set; }
        public string FIO { get; set; }
        public Nullable<System.DateTime> date_born { get; set; }
        public string adres { get; set; }
        public Nullable<int> id_group { get; set; }
        public string school { get; set; }
        public string national { get; set; }
        public string phone { get; set; }
        public string health { get; set; }
        public string relationship { get; set; }
        public byte[] photo { get; set; }
        public string hobby { get; set; }
        public string expelled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Brothers_sisters> Brothers_sisters { get; set; }
        public virtual Groups Groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hostel> Hostel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parents> Parents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Passport> Passport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Works_parent> Works_parent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Works_student> Works_student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Achivment_student> Achivment_student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facts_assotial> Facts_assotial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Hobbies> Student_Hobbies { get; set; }
    }
}
