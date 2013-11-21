using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace Bros.Models
{
    [Table("User")]
    public class User : Entity
    {

        public byte[] salt { get; set; }
        public string Email { get; set; }
        public byte[] password { get; set; }
        
        [InverseProperty("Id")]
        [ForeignKey("BlockedUser")]
		public virtual ICollection<User> BlockedBros { get; set; }
        [Required]
        [InverseProperty("Id")]
        [ForeignKey("Profile")]
        public virtual Profile Profile { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Circle")]
        public virtual ICollection<Circle> Circles { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("SentRequest")]
        public virtual ICollection<BroRequest> SentBroRequests { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("ReceivedRequest")]
        public virtual ICollection<BroRequest> ReceivedBroRequest { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Notification")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Message")]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Post")]
        public virtual ICollection<Post> Posts { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Comment")]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty("Id")]
        [ForeignKey("Preference")]
        public virtual ICollection<Preference> Preferences { get; set; }

        public static byte[] GeneratedSaltedHash(string plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] plainTextWithSaltedBytes = new byte[plainTextBytes.Length + salt.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
            {
                plainTextWithSaltedBytes[i] = plainTextBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltedBytes[i + plainTextBytes.Length] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltedBytes);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        public static byte[] CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[size];
            rng.GetBytes(salt);

            return salt;
        }
    }
}