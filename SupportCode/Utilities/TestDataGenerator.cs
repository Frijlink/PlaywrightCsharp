public class TestDataGenerator
{
    public string GenerateBoardName()
    {
        return string.Join('_', Faker.Lorem.Words(3));
    }
}