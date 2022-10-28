using System;
using System.Collections.Generic;
using System.Text;
using Hs.CrossCutting.Security;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hs.Infrastructure.Helpers
{
    public static class EfHelper
    {
       

        /// <summary>
        /// https://zenu.wordpress.com/2011/09/21/aes-128bit-cross-platform-java-and-c-encryption-compatibility/
        /// /https://www.dotnettips.info/post/3015/%d8%b1%d9%85%d8%b2%d9%86%da%af%d8%a7%d8%b1%db%8c-%d8%ae%d9%88%d8%af%da%a9%d8%a7%d8%b1-%d9%81%db%8c%d9%84%d8%af%d9%87%d8%a7-%d8%aa%d9%88%d8%b3%d8%b7-entity-framework-core
        /// </summary>
        /// <returns></returns>
        /// 
        public static ValueConverter Converter()
        {

            //var t= AesCryptography.Decrypt("LpFggntYqwiWVU0WvpSnPVu4KTqpRwNJ/eZf4ETs4NOsfim7J/QMZaOHLjXvbTL2MIsMMIkr6HEbAS6WipxuCUP7gfuBVWfhupXV6zdIJfHE3H6W435eBuxqW3BbBkDSq9OwIynZMBtUwjCg2hqm5Iq3 / JzdJ3xNrzgWN09aTotJWBDzaE + psPWhS / B5Csa / Zh7rTijT + G7MnWxcVQifkWyyJ7DxhV7DmOWCw63BV4nMphHDpEecPemE7v6l8or / FL / IZcXBOW0n9kBFjY2K2 / VWa2qH / 3zszT9KYbtxcNfGOmaoQe69cBgPUE35oQ / ClFqgI49gE67I0sawJ6yMvjJ6VYGQKU0e0XDlRUKt1LQIFnGzj9B6rG + fwTFvmi5XHJdIjgvkcPKJX1DBUbVZV2d7eRLidE2h0 / YYmfy7p + HLtNOjbfGX033ZfAI3SD0c0XDHkGFyHmBlBdxTHHy7vCEObFXThLD + Qr9dJgwyi7TDaEt2Y4aK15PfxENm12yQUzlQYaJJE9ASSFNIjHjxRuKKKAbaKr7f3oscbE7EZjZfWqkdIdSjBOCuamwDBlmUplbxYdpiIp5y11WLdaTrtD + WC1dE1o7NrN6 / f6qxpF6kTpZtcj + VXg0dvXwN8FVcWFnW59RuxRTptGqks1lGsab66bF1fQwFYQJ84zZ4F0zGpRIGvGTVtInOSgJwgVz / tGoLNIEcPIOPAj7hP4QkG4aaSX9pUZpcqKfAHJk + GZrs6WbAa + jU9SxT69hF2WgKXXFSSOFDhnoQSktUbtSd / HLRcvx07Nl2A5SDC7u6EKx9YsXkrT83gYyXMArM + ywbCMduJ8xZe / arXCXyWUU3OwQ6H4RBYZDk9cP / nxppncOzmyrolqrrtGXVoqj3sa / m70OW7DSUWmi941AlJytjyCgU8rZg0LZkHHZV957nhT9lFgEbmSUUfXpPA9YVmrWkmR / ihUgdgv1nYToST8Cn1bQrU6jqZg5k73fg6QpiTlV31R3D5NwmiuWf1GbnZ82h / pnQeYLH6LvCCmhFv4pIUAIS3WTo3T3FapAIw11qq9Ybyf2yyw9KYTmaAyT / F6M7 / e3P3wQYxSNVoIjQV7N1 / SqIbKnxXWhyZ75K / vJ73z8C / 5NscBSjPFkJcNouwD / QDHYs6zpsR1p7fWCTGFmU4r7vbwWLQ3EVP + Vx3 / I0Qxw = ");
            var encryptedConverter = new ValueConverter<string, string>(
                convertToProviderExpression: v =>
                    new string(v.Decrypt()), // encrypt before saving to database
                convertFromProviderExpression: v =>
                    new string(v.Encrypt()) // decrypt after fetching from database
            );

            return encryptedConverter;
        }
    }
}
