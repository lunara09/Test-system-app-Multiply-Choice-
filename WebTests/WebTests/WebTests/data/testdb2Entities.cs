namespace WebTests.data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class testdb2Entities : DbContext
    {
        public testdb2Entities()
            : base("name=testdb2Entities")
        {
        }

        public virtual DbSet<ANSWER> ANSWER { get; set; }
        public virtual DbSet<QA> QA { get; set; }
        public virtual DbSet<QUESTION> QUESTION { get; set; }
        public virtual DbSet<TEST> TEST { get; set; }
        public virtual DbSet<USER> USER { get; set; }
        public virtual DbSet<USERANSWERS> USERANSWERS { get; set; }
        public virtual DbSet<USERTEST> USERTEST { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ANSWER>()
                .Property(e => e.ANSWERTEXT)
                .IsUnicode(false);

            modelBuilder.Entity<ANSWER>()
                .Property(e => e.ANSWERNOTE)
                .IsUnicode(false);

            modelBuilder.Entity<QA>()
                .HasMany(e => e.USERANSWERS)
                .WithRequired(e => e.QA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QUESTION>()
                .Property(e => e.QUESTIONTEXT)
                .IsUnicode(false);

            modelBuilder.Entity<QUESTION>()
                .Property(e => e.QUESTIONNOTE)
                .IsUnicode(false);

            modelBuilder.Entity<TEST>()
                .Property(e => e.TESTCATERY)
                .IsUnicode(false);

            modelBuilder.Entity<TEST>()
                .Property(e => e.TESTTITLE)
                .IsUnicode(false);

            modelBuilder.Entity<TEST>()
                .Property(e => e.TESTNOTE)
                .IsUnicode(false);

            modelBuilder.Entity<TEST>()
                .HasMany(e => e.QUESTION)
                .WithRequired(e => e.TEST)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TEST>()
                .HasMany(e => e.USERTEST)
                .WithRequired(e => e.TEST)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.LNAME)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.FNAME)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.USERTEST)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USERANSWERS>()
                .Property(e => e.NOTE)
                .IsUnicode(false);

            modelBuilder.Entity<USERTEST>()
                .Property(e => e.TESTREMARKS)
                .IsUnicode(false);

            modelBuilder.Entity<USERTEST>()
                .HasMany(e => e.USERANSWERS)
                .WithRequired(e => e.USERTEST)
                .WillCascadeOnDelete(false);
        }
    }
}
