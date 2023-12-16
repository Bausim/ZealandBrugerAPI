using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZealandBrugerAPI
{
    public class Bruger
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public bool Admin { get; set; }
        [Required]
        [StringLength(50)]
        public string Brugernavn { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }


        public void Validate()
        {
            ValidateBrugernavn();
            ValidatePassword();
        }

        public void ValidateBrugernavn()
        {
            if (Brugernavn == null) { throw new ArgumentNullException("Brugernavn må ikke være null"); }
            if (Brugernavn.Length > 50) { throw new ArgumentOutOfRangeException("Brugernavn må ikke være mere en 50 charactere"); }
        }

        public void ValidatePassword()
        {
            if (Password == null) { throw new ArgumentNullException("Kodeordet må ikke være null"); }
            if (Password.Length > 50) { throw new ArgumentOutOfRangeException("Kodeordet må ikke være mere en 50 charactere"); }
        }





        public override bool Equals(object? obj)
        {
            return obj is Bruger bruger &&
                   Admin == bruger.Admin &&
                   Brugernavn == bruger.Brugernavn &&
                   Password == bruger.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Admin, Brugernavn, Password);
        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Admin)}={Admin.ToString()}, {nameof(Brugernavn)}={Brugernavn}, {nameof(Password)}={Password}}}";
        }
    }
}
