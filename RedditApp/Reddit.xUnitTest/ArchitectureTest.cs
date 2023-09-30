using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using ArchUnitNET.xUnit;

namespace Reddit.xUnitTest
{
    public class ArchitectureTest
    {
        private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(
               System.Reflection.Assembly.Load("Reddit.Domain"),
               System.Reflection.Assembly.Load("Reddit.Services"),
               System.Reflection.Assembly.Load("Reddit.Web"),
               System.Reflection.Assembly.Load("Reddit.Data")).Build();

        private readonly IObjectProvider<IType> DomainModelLayer = Types().That().ResideInAssembly("Reddit.Domain");

        private readonly IObjectProvider<IType> DomainServiceLayer = Types().That().ResideInNamespace("Reddit.Services");

        private readonly IObjectProvider<IType> RedditDataLayer = Types().That().ResideInNamespace("Reddit.Data");

        private readonly IObjectProvider<IType> PresentationLayer = Types().That().ResideInNamespace("Reddit.Web");


        [Fact]
        public void TestLayerForViolations()
        {
            //The domain layer should not have reference to domain service.
            IArchRule layerdRule = Types().That().Are(DomainModelLayer)
                .Should().NotDependOnAny(DomainServiceLayer)
                .Because("The domain layer should not have reference to any domain service");
            layerdRule.Check(Architecture);

            //Domain layer should not have references to the reddit data layer.
            layerdRule = Types().That().Are(DomainModelLayer)
               .Should().NotDependOnAny(RedditDataLayer)
               .Because("Domain layer should not have references to the reddit data layer.");
            layerdRule.Check(Architecture);

            //Domain layer should not have references to the Presentation layer.
            layerdRule = Types().That().Are(DomainModelLayer)
               .Should().NotDependOnAny(PresentationLayer)
               .Because("Domain layer should not have references to the Presentation layer.");
            layerdRule.Check(Architecture);

            //The Presentation Layer should have reference to domain layer
            layerdRule = Types().That().Are(PresentationLayer)
                .Should().DependOnAny(DomainModelLayer);

            layerdRule.Check(Architecture);
        }
    }
}