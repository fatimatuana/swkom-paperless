/*
 * Paperless Rest Server
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ApiDocumentTypesBody : IEquatable<ApiDocumentTypesBody>
    { 
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]

        [DataMember(Name="name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets MatchingAlgorithm
        /// </summary>
        [Required]

        [DataMember(Name="matching_algorithm")]
        public int? MatchingAlgorithm { get; set; }

        /// <summary>
        /// Gets or Sets Match
        /// </summary>
        [Required]

        [DataMember(Name="match")]
        public string Match { get; set; }

        /// <summary>
        /// Gets or Sets IsInsensitive
        /// </summary>
        [Required]

        [DataMember(Name="is_insensitive")]
        public bool? IsInsensitive { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [Required]

        [DataMember(Name="owner")]
        public int? Owner { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ApiDocumentTypesBody {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  MatchingAlgorithm: ").Append(MatchingAlgorithm).Append("\n");
            sb.Append("  Match: ").Append(Match).Append("\n");
            sb.Append("  IsInsensitive: ").Append(IsInsensitive).Append("\n");
            sb.Append("  Owner: ").Append(Owner).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ApiDocumentTypesBody)obj);
        }

        /// <summary>
        /// Returns true if ApiDocumentTypesBody instances are equal
        /// </summary>
        /// <param name="other">Instance of ApiDocumentTypesBody to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ApiDocumentTypesBody other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) && 
                (
                    MatchingAlgorithm == other.MatchingAlgorithm ||
                    MatchingAlgorithm != null &&
                    MatchingAlgorithm.Equals(other.MatchingAlgorithm)
                ) && 
                (
                    Match == other.Match ||
                    Match != null &&
                    Match.Equals(other.Match)
                ) && 
                (
                    IsInsensitive == other.IsInsensitive ||
                    IsInsensitive != null &&
                    IsInsensitive.Equals(other.IsInsensitive)
                ) && 
                (
                    Owner == other.Owner ||
                    Owner != null &&
                    Owner.Equals(other.Owner)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                    if (MatchingAlgorithm != null)
                    hashCode = hashCode * 59 + MatchingAlgorithm.GetHashCode();
                    if (Match != null)
                    hashCode = hashCode * 59 + Match.GetHashCode();
                    if (IsInsensitive != null)
                    hashCode = hashCode * 59 + IsInsensitive.GetHashCode();
                    if (Owner != null)
                    hashCode = hashCode * 59 + Owner.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ApiDocumentTypesBody left, ApiDocumentTypesBody right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ApiDocumentTypesBody left, ApiDocumentTypesBody right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}