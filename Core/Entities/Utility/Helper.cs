//using NepFlex.Core.Entities.Utility;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml.Serialization;

//namespace NepFlex.Core.Entities.ResourceModels
//{
//    public static class Helper
//    {
//        public static V GetValueFromDictionary<K, V>(Dictionary<K, V> dic, K key)
//        {
//            bool found = dic.TryGetValue(key, out V ret);
//            if (found)
//            {
//                return ret;
//            }
//            return default(V);
//        }
//        public static string FormatPhoneNumber(string phoneNumber)
//        {
//            int phoneLen = 0;
//            string formatNumber = string.Empty;
//            try
//            {
//                if (!string.IsNullOrEmpty(phoneNumber))
//                {
//                    phoneLen = phoneNumber.ToString().Trim().Length;
//                }
//                switch (phoneLen)
//                {
//                    case 11:
//                        formatNumber = phoneNumber.Substring(0, 1) + "-" + phoneNumber.Substring(1, 3) + "-" + phoneNumber.Substring(4, 3) + "-" + phoneNumber.Substring(7, 4);
//                        break;
//                    case 10:
//                        formatNumber = "1-" + phoneNumber.Substring(0, 3) + "-" + phoneNumber.Substring(3, 3) + "-" + phoneNumber.Substring(6, 4);
//                        break;
//                    case 7:
//                        formatNumber = phoneNumber.Substring(0, 3) + "-" + phoneNumber.Substring(3, 4);
//                        break;
//                    default:
//                        formatNumber = phoneNumber;
//                        break;
//                }
//                return formatNumber;
//            }
//            catch (Exception ex)
//            {
//                //LogError(ex.ToString) :
//                return phoneNumber;
//            }
//        }
//        public static string FormatNumber(decimal value, int numOfDecimalsToTruncate, bool isShares = false)
//        {
//            string str = string.Empty;
//            try
//            {
//                str = value.ToString();
//                int index = str.IndexOf(".");
//                string strNumberPart = string.Empty;
//                int lenDecimals = 0;
//                string decimalPart = string.Empty;
//                if (index < 0)
//                {
//                    str = str + ".00";
//                    if (str.IndexOf("$") < 0)
//                    {
//                        str = string.Format("${0}", str);
//                    }
//                }
//                else
//                {
//                    strNumberPart = str.Substring(0, index);
//                    lenDecimals = str.Length - index - 1;
//                    if (lenDecimals <= 1)
//                    {
//                        if (numOfDecimalsToTruncate == 2)
//                        {
//                            decimalPart = str.Substring(index + 1, Math.Min(lenDecimals, numOfDecimalsToTruncate)) + "0";
//                        }
//                        if (numOfDecimalsToTruncate == 3)
//                        {
//                            decimalPart = str.Substring(index + 1, Math.Min(lenDecimals, numOfDecimalsToTruncate)) + "00";
//                        }
//                        if (numOfDecimalsToTruncate == 4)
//                        {
//                            decimalPart = str.Substring(index + 1, Math.Min(lenDecimals, numOfDecimalsToTruncate)) + "000";
//                        }
//                    }
//                    else
//                    {
//                        decimalPart = str.Substring(index + 1, Math.Min(lenDecimals, numOfDecimalsToTruncate));
//                    }
//                    str = string.Format("{0}.{1}", Math.Abs(Convert.ToInt64(strNumberPart)).ToString("N0"), decimalPart);

//                    if (!isShares)
//                    {
//                        if (strNumberPart == "-0")
//                        {
//                            str = string.Format("-${0}", str);
//                            return str;
//                        }
//                        if (Convert.ToInt64(strNumberPart) < 0)
//                        {
//                            str = string.Format("-${0}", str);
//                        }
//                        else
//                        {
//                            str = string.Format("${0}", str);
//                        }
//                    }
//                    else
//                    {
//                        if (strNumberPart == "-0")
//                        {
//                            str = string.Format("-{0}", str);
//                            return str;
//                        }
//                        if (Convert.ToInt64(strNumberPart) < 0)
//                        {
//                            str = string.Format("-{0}", str);
//                        }
//                        else
//                        {
//                            str = string.Format("{0}", str);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//                //.LogError(ex.ToString());
//                if (!isShares)
//                {
//                    str = '$' + value.ToString();
//                }
//                else
//                {
//                    str = value.ToString();
//                }
//            }
//            return str;
//        }

//        /// <summary>
//        /// formats string currency to string
//        /// </summary>
//        /// <param name="value">string</param>
//        /// <returns>currency in string format</returns>
//        public static string FormatCurrency(string value)
//        {
//            try
//            {
//                Decimal _decimalVal = 0;
//                bool IsValid = decimal.TryParse(value, out _decimalVal);
//                if (IsValid)
//                {
//                    int size = value.Contains('.') ? value.Split('.')[1].Length : 2;
//                    string result = "{0:C" + String.Format("{0}", size) + "}";
//                    return String.Format(result, _decimalVal);
//                }
//                else
//                {
//                    return value;
//                }
//            }
//            catch (Exception ex)
//            {
//                //logError(ex.ToString());
//                return value;
//            }
//        }

//        /// <summary>
//        /// Converts the decimal value to formatted string with comma seperator
//        /// </summary>
//        /// <param name="source">decimal source</param>
//        /// <param name="decimalPlaces">integer type decimal places</param>
//        /// <returns>Comma formatted string</returns>
//        public static string ToNumberFormatting(this decimal source, int decimalPlaces)
//        {
//            return source.ToString("N" + decimalPlaces, CultureInfo.CurrentCulture);
//        }

//        /// <summary>
//        /// converts integer value to formatted string with comma seperator 
//        /// </summary>
//        /// <param name="source">integer source</param>
//        /// <param name="decimalPlaces">integer type decimal places</param>
//        /// <returns>Comma formatted string</returns>
//        public static string ToNumberFormatting(this int source, int decimalPlaces)
//        {
//            return source.ToString("N" + decimalPlaces, CultureInfo.CurrentCulture);
//        }

//        /// <summary>
//        /// Converts currency from decimal to string
//        /// </summary>
//        /// <param name="source">deciaml</param>
//        /// <param name="decimalPlaces">integer</param>
//        /// <returns>deciaml to string currency format</returns>
//        public static string ToCurrencyFormatting(this decimal source, int decimalPlaces)
//        {
//            return source.ToString("C" + decimalPlaces, CultureInfo.CurrentCulture);
//        }

//        /// <summary>
//        /// Converts currency from integer to string
//        /// </summary>
//        /// <param name="source">integer</param>
//        /// <param name="decimalPlaces">integer</param>
//        /// <returns>integer to string currency format</returns>
//        public static string ToCurrencyFormatting(this int source, int decimalPlaces)
//        {
//            return source.ToString("C" + decimalPlaces, CultureInfo.CurrentCulture);
//        }

//        /// <summary>
//        /// Converts string source to decimal
//        /// </summary>
//        /// <param name="source">formatted input string</param>
//        /// <returns>decimal</returns>
//        public static decimal ToDecimal(this string source)
//        {
//            decimal target = 0;

//            if (!string.IsNullOrWhiteSpace(source))
//            {
//                source = source.Replace("(", "").Replace(")", "");
//                decimal.TryParse(source, NumberStyles.Any, CultureInfo.CurrentCulture, out target);
//            }
//            return target;
//        }
//        /// <summary>
//        /// Converts string to Integer
//        /// </summary>
//        /// <param name="source">formatted input string</param>
//        /// <returns>integer</returns>
//        public static decimal ToInteger(this string source)
//        {
//            int target = 0;

//            if (!string.IsNullOrWhiteSpace(source))
//            {
//                source = source.Replace("(", "").Replace(")", "");
//                int.TryParse(source, NumberStyles.Any, CultureInfo.CurrentCulture, out target);
//            }
//            return target;
//        }
//        public static string GetSerialized(object req)
//        {
//            StringBuilder _serialize = new StringBuilder();
//            XmlSerializer _xmlSerialize = new XmlSerializer(req.GetType());
//            StringWriter _writer = new StringWriter(_serialize);
//            //XmlSerializer.Serialize(_writer, req); //need to research why is this not working
//            return _serialize.ToString();

//        }
//        public static string Serialize(object source)
//        {
//            StringBuilder sb = new StringBuilder();
//            if (source != null)
//            {
//                using (System.IO.StringWriter sw = new StringWriter(sb))
//                {
//                    XmlSerializer xs = new XmlSerializer(source.GetType());
//                    xs.Serialize(sw, source);
//                }
//            }
//            return sb.ToString();
//        }

//        public static object DeSerialize(string source, Type type)
//        {
//            object serializedObj = null;
//            using (StringReader sr = new StringReader(source))
//            {
//                XmlSerializer xs = new XmlSerializer(type);
//                serializedObj = xs.Deserialize(sr);
//            }

//            return serializedObj;
//        }
//        public static bool ConvertStringToBool(string value)
//        {
//            if (string.IsNullOrEmpty(value) || value.Length == 0)
//            {
//                return false;
//            }
//            else
//            {
//                if (string.Equals(value, "Y", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "Yes", StringComparison.OrdinalIgnoreCase))
//                {
//                    return true;
//                }
//            }
//            return false;
//        }
//        public static void LogWarningMessage(string message)
//        {
//            LogResposneMessage(string.Empty, null, message, false);
//        }
//        public static void LogErrorMessage(string message, Exception ex = null)
//        {
//            LogResposneMessage(string.Empty, ex, message, true);
//        }
//        public static void LogResposneMessage(string message, Exception ex = null, string errorMessage = "", bool isError = true)
//        {
//            if (ex != null)
//            {
//                //db call log error
//            }

//            if (!string.IsNullOrEmpty(errorMessage))
//            {
//                if (isError)
//                {
//                    //db log error(errorMessage)
//                }
//                {
//                    //db log warning(errorMessage)
//                }
//            }
//        }
//        public static string LogTransaction(Utility.TransactionStatus _status, string _tranName, string _arg, string _transactionId = null)
//        {
//            string tranId = string.Empty;
//            var tranDetail = new LogTransactionDetail
//            {
//                TranArg = _arg,
//                TranDetail = string.Empty,
//                TranId = _transactionId,
//                TranStatus = _status,
//                TranTitle = _tranName,
//                UI = CONSTUINAME.UI_NAME,
//                UserLocation = System.Net.Dns.GetHostName()
//            };

//            if (!string.IsNullOrEmpty(_transactionId))
//            {
//                tranDetail.TranId = _transactionId;
//            }

//            try
//            {
//                List<string> diagnostics;
//                Utility.LogTransaction(tranDetail, out tranId, out diagnostics);
//            }
//            catch (Exception ex)
//            {
//                //LogWarning(ex.ToString());
//            }
//            return tranId;
//        }
        
        
//        /// <summary>
//        /// Generate random digit code
//        /// </summary>
//        /// <param name="length">Length</param>
//        /// <returns>Result string</returns>
//        public static string GenerateRandomDigitCode(int length)
//        {
//            var random = new RandomNumGenerator();
//            var str = string.Empty;
//            for (var i = 0; i < length; i++)
//                str = string.Concat(str, random.Next(10).ToString());
//            return str;
//        }

//    }
//}

