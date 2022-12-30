public class TestDataGenerator
{
    public string GenerateBoardName()
    {
        return String.Join('_', Faker.Lorem.Words(3));
    }
}