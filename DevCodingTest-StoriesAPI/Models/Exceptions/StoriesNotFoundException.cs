namespace DevCodingTest_StoriesAPI.Models.Exceptions
{
    public sealed class StoriesNotFoundException : NotFoundException
    {
        public StoriesNotFoundException()
            : base($"The stories don't exist in the source.")
        {

        }
    }
}
