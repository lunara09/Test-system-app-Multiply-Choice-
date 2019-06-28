namespace WebTests.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QA")]
    public partial class QA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QA()
        {
            USERANSWERS = new HashSet<USERANSWERS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QA_ID { get; set; }

        public int? QUESTIONID { get; set; }

        public int? ANSWERID { get; set; }

        public bool? CORRECTANSWER { get; set; }

        public virtual ANSWER ANSWER { get; set; }

        public virtual QUESTION QUESTION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERANSWERS> USERANSWERS { get; set; }
    }
}
