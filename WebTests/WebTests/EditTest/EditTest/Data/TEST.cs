namespace EditTest.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TEST")]
    public partial class TEST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEST()
        {
            QUESTION = new HashSet<QUESTION>();
            USERTEST = new HashSet<USERTEST>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TESTID { get; set; }

        [StringLength(50)]
        public string TESTCATERY { get; set; }

        [StringLength(50)]
        public string TESTTITLE { get; set; }

        [StringLength(200)]
        public string TESTNOTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUESTION> QUESTION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERTEST> USERTEST { get; set; }
    }
}
