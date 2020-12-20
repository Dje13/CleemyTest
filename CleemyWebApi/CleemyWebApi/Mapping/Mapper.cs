
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CleemyWebApi.Mapping
{
    public class Mapper
    {

        /// <summary>
        /// Map object with reflexion on scalar attributes
        /// </summary>
        /// <param name="SourceObject"></param>
        /// <param name="TargetObject"></param>
        /// <param name="WithId">True :  ID is mapped otherwise ID field is ignored</param>
        /// <param name="unMappedField">Exception fields not to be mapped</param>
        public static void mapObject(object SourceObject, object TargetObject, bool WithId = false, List<string> unMappedField = null)
        {
            if (unMappedField == null)
            {
                unMappedField = new List<string>();
            }
            PropertyInfo[] SourceProps = SourceObject.GetType().GetProperties();
            PropertyInfo[] TargetProps = TargetObject.GetType().GetProperties();

            for (int i = 0; i < SourceProps.Length; i++)
            {
                PropertyInfo DTOProp = TargetProps.Where(s => s.Name.ToLower() == SourceProps[i].Name.ToLower() && s.PropertyType.Namespace == "System").FirstOrDefault();

                if (unMappedField.Where(s => s.ToLower() == SourceProps[i].Name.ToLower()).FirstOrDefault() == null && SourceProps[i].PropertyType.Namespace == "System" && (DTOProp != null) && DTOProp.CanWrite && (SourceProps[i].Name.ToLower() != "id" || WithId))
                {
                    DTOProp.SetValue(TargetObject, SourceProps[i].GetValue(SourceObject, null));
                }
            }
        }


        

    }
}
