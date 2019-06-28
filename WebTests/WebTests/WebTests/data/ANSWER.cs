namespace WebTests.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ANSWER")]
    public partial class ANSWER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ANSWER()
        {
            QA = new HashSet<QA>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ANSWERID { get; set; }

        [StringLength(200)]
        public string ANSWERTEXT { get; set; }

        [StringLength(150)]
        public string ANSWERNOTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QA> QA { get; set; }
    }
}
