﻿namespace VSGBulgariaMarketplace.Application.Services.HelpServices
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public static class EnumService
    {
        public static string GetEnumDisplayName(Enum enumValue)
        {
            var enumMember = enumValue.GetType().GetMember(enumValue.ToString())[0];

            var displayAttribute = enumMember.GetCustomAttribute<DisplayAttribute>();

            return displayAttribute != null ? displayAttribute.Name : enumValue.ToString();
        }

        public static T GetEnumValueFromDisplayName<T>(string displayName) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == displayName)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == displayName)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException($"No {typeof(T)} with display name {displayName} found");
        }
    }
}
