using FindMe.Domain.Constants;
using System.Reflection.Metadata.Ecma335;


namespace FindMe.Application.Common.Mapping.Helpers
{
    public class Map
    {
        public static Gendre? MapGender(string? gender)
        {
            if(gender is null)
                return null;

            return gender.ToLower() switch
            {
                "male" => Gendre.Male,
                "female" => Gendre.Female,
                _ => throw new ArgumentException($"Invalid gender value: {gender}"),
            };
        }

        public static MatiralStatus MapMatiralStatus(string status)
        {
            return status.ToLower() switch
            {
                "single" => MatiralStatus.Single,
                "married" => MatiralStatus.Married,
                "devorced" => MatiralStatus.Devorced,
                _=> throw new ArgumentException($"Invalid MatiralStatus value: {status}")
            };
        }

        public static Relationship MapRelationship(string relation)
        {
            return relation.ToLower() switch
            {
                "daughter" => Relationship.Daughter,
                "mother" => Relationship.Mother,
                "sister" => Relationship.Sister,
                "son" => Relationship.Son,
                "father" => Relationship.Father,
                "brother" => Relationship.Brother,
                "wife" => Relationship.Wife,
                _=> throw new ArgumentException($"Invalid Relationship value: {relation}")

            };
        }


    }
}
