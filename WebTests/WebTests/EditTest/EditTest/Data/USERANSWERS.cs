namespace EditTest.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERANSWERS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int USERANSWERSID { get; set; }
        public int USERTESTID { get; set; }

        public int QA_ID { get; set; }

        [StringLength(200)]
        public string NOTE { get; set; }

        public virtual QA QA { get; set; }

        public virtual USERTEST USERTEST { get; set; }
    }
}
