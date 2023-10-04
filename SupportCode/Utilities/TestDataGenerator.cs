public static class TestDataGenerator
{
    public static string GenerateBoardName()
    {
        return string.Join('_', Faker.Lorem.Words(3));
    }
}