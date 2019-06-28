namespace WebTests.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERTEST")]
    public partial class USERTEST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USERTEST()
        {
            USERANSWERS = new HashSet<USERANSWERS>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int USERTESTID { get; set; }

        public int USERID { get; set; }

        public int TESTID { get; set; }

        public DateTime? TESTDATE { get; set; }

        [StringLength(150)]
        public string TESTREMARKS { get; set; }

        public virtual TEST TEST { get; set; }

        public virtual USER USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERANSWERS> USERANSWERS { get; set; }
    }
}
