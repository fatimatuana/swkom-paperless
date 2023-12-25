/*
 * Swagger Petstore - OpenAPI 3.0
 *
 * This is a sample Pet Store Server based on the OpenAPI 3.0 specification.  You can find out more about Swagger at [http://swagger.io](http://swagger.io). In the third iteration of the pet store, we've switched to the design first approach! You can now help us improve the API whether it's by making changes to the definition itself or to the code. That way, with time, we can improve the API in general, and expose some of the new features in OAS3.  Some useful links: - [The Pet Store repository](https://github.com/swagger-api/swagger-petstore) - [The source API definition for the Pet Store](https://github.com/swagger-api/swagger-petstore/blob/master/src/main/resources/openapi.yaml)
 *
 * The version of the OpenAPI document: 1.0.17
 * Contact: apiteam@swagger.io
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Org.OpenAPITools.Converters;

namespace Org.OpenAPITools.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Order : IEquatable<Order>
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        /// <example>10</example>
        [DataMember(Name="id", EmitDefaultValue=true)]
        public long Id { get; set; }

        /// <summary>
        /// Gets or Sets PetId
        /// </summary>
        /// <example>198772</example>
        [DataMember(Name="petId", EmitDefaultValue=true)]
        public long PetId { get; set; }

        /// <summary>
        /// Gets or Sets Quantity
        /// </summary>
        /// <example>7</example>
        [DataMember(Name="quantity", EmitDefaultValue=true)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or Sets ShipDate
        /// </summary>
        [DataMember(Name="shipDate", EmitDefaultValue=false)]
        public DateTime ShipDate { get; set; }


        /// <summary>
        /// Order Status
        /// </summary>
        /// <value>Order Status</value>
        [TypeConverter(typeof(CustomEnumConverter<StatusEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum PlacedEnum for placed
            /// </summary>
            [EnumMember(Value = "placed")]
            PlacedEnum = 1,
            
            /// <summary>
            /// Enum ApprovedEnum for approved
            /// </summary>
            [EnumMember(Value = "approved")]
            ApprovedEnum = 2,
            
            /// <summary>
            /// Enum DeliveredEnum for delivered
            /// </summary>
            [EnumMember(Value = "delivered")]
            DeliveredEnum = 3
        }

        /// <summary>
        /// Order Status
        /// </summary>
        /// <value>Order Status</value>
        /// <example>approved</example>
        [DataMember(Name="status", EmitDefaultValue=true)]
        public StatusEnum Status { get; set; }

        /// <summary>
        /// Gets or Sets Complete
        /// </summary>
        [DataMember(Name="complete", EmitDefaultValue=true)]
        public bool Complete { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Order {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  PetId: ").Append(PetId).Append("\n");
            sb.Append("  Quantity: ").Append(Quantity).Append("\n");
            sb.Append("  ShipDate: ").Append(ShipDate).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Complete: ").Append(Complete).Append("\n");
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
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Order)obj);
        }

        /// <summary>
        /// Returns true if Order instances are equal
        /// </summary>
        /// <param name="other">Instance of Order to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Order other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    
                    Id.Equals(other.Id)
                ) && 
                (
                    PetId == other.PetId ||
                    
                    PetId.Equals(other.PetId)
                ) && 
                (
                    Quantity == other.Quantity ||
                    
                    Quantity.Equals(other.Quantity)
                ) && 
                (
                    ShipDate == other.ShipDate ||
                    ShipDate != null &&
                    ShipDate.Equals(other.ShipDate)
                ) && 
                (
                    Status == other.Status ||
                    
                    Status.Equals(other.Status)
                ) && 
                (
                    Complete == other.Complete ||
                    
                    Complete.Equals(other.Complete)
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
                    
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    
                    hashCode = hashCode * 59 + PetId.GetHashCode();
                    
                    hashCode = hashCode * 59 + Quantity.GetHashCode();
                    if (ShipDate != null)
                    hashCode = hashCode * 59 + ShipDate.GetHashCode();
                    
                    hashCode = hashCode * 59 + Status.GetHashCode();
                    
                    hashCode = hashCode * 59 + Complete.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Order left, Order right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Order left, Order right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}