﻿namespace DAX.Runtime.Extensions.CRTExtensions.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;

    /// <summary>
    /// Helper class for custom shopping cart related workflows.
    /// </summary>
    public static class CustomCartHelper
    {
        /// <summary>
        /// Updates the transaction header attribute.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="reserveNow">The value of the transaction header attribute.</param>
        /// <param name="updateAttribute">A flag indicating whether or not to override an existing attribute value.</param>
        /// <returns>A flag indication whether or not the cart was updated.</returns>
        public static bool CreateUpdateTransactionHeaderAttribute(Cart cart, bool reserveNow, bool updateAttribute)
        {
            ThrowIf.Null(cart, "cart");
            bool cartUpdated = false;
            IList<AttributeValueBase> transactionAttributes = cart.AttributeValues;
            string reserveNowAttributeName = "Reserve now";
            string reserveNowAttributeValue = reserveNow ? "Yes" : "No";
            AttributeValueBase reserveNowAttribute = transactionAttributes.SingleOrDefault(attribute => attribute.Name.Equals(reserveNowAttributeName));

            if (reserveNowAttribute == null)
            {
                transactionAttributes.Add(new AttributeTextValue() { Name = reserveNowAttributeName, TextValue = reserveNowAttributeValue });
                cartUpdated = true;
            }
            else if (updateAttribute && !((AttributeTextValue)reserveNowAttribute).TextValue.Equals(reserveNowAttributeValue))
            {
                ((AttributeTextValue)reserveNowAttribute).TextValue = reserveNowAttributeValue;
                cartUpdated = true;
            }

            return cartUpdated;
        }
    }
}
