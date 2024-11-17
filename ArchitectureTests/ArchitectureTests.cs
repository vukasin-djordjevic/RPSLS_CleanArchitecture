using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string PresentationNamespace = "Presentation";
        private const string WebNamespace = "WebApi";


        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var othersProjects = new[] { ApplicationNamespace, InfrastructureNamespace, PresentationNamespace, WebNamespace };

            // Act
            var result = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(othersProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var othersProjects = new[] { InfrastructureNamespace, PresentationNamespace, WebNamespace };

            // Act
            var result = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(othersProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Handlers_Should_Have_DependencyOnDomain()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            // Act
            var result = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var othersProjects = new[] { PresentationNamespace, WebNamespace };

            // Act
            var result = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(othersProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;

            var othersProjects = new[] { InfrastructureNamespace, WebNamespace };

            // Act
            var result = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(othersProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Controllers_Should_HaveDependencyOnMediatR()
        {
            // Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;


            // Act
            var result = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .Should()
                .HaveDependencyOn("MediatR")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }


    }
}
