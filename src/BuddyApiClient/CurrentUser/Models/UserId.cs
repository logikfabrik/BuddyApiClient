namespace BuddyApiClient.CurrentUser.Models
{
    [StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
    public partial struct UserId
    {
    }
}