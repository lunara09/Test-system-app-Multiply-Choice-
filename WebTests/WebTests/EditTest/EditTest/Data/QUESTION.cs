namespace EditTest.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QUESTION")]
    public partial class QUESTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QUESTION()
        {
            QA = new HashSet<QA>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QUESTIONID { get; set; }

        public int TESTID { get; set; }

        [StringLength(200)]
        public string QUESTIONTEXT { get; set; }

        [StringLength(150)]
        public string QUESTIONNOTE { get; set; }

        public int? QUESTIONNUMBER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QA> QA { get; set; }

        public virtual TEST TEST { get; set; }
    }
}
