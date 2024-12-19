using System;
using System.Security.Cryptography;
using System.Text;
using Moq;
using NUnit.Framework;
using AutoMapper;
using FluentValidation;
using Application.Authentication;
using Application.Dtos.UserDtos;
using Application.Services;
using Domain.Abstractions;
using FluentValidation;
using FluentValidation.Results;
using Domain.Entities;
using Domain.Enums;
using Application.Exceptions;

namespace ApplicationTests.Services;

[TestFixture]
public class UserServiceTests
{
    private UserService _userService;
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<IJwtProvider> _jwtProviderMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IValidator<RegisterDto>> _registerDtoValidatorMock;
    private Mock<IValidator<AuthenticationRequest>> _authenticationRequestValidatorMock;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtProviderMock = new Mock<IJwtProvider>();
        _mapperMock = new Mock<IMapper>();
        _registerDtoValidatorMock = new Mock<IValidator<RegisterDto>>();
        _authenticationRequestValidatorMock = new Mock<IValidator<AuthenticationRequest>>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _jwtProviderMock.Object,
            _mapperMock.Object,
            _registerDtoValidatorMock.Object,
            _authenticationRequestValidatorMock.Object);
    }

    [Test]
    public void CreatePassword_ShouldGenerateHashAndSalt()
    {
        // Arrange
        string password = "TestPassword";
        byte[] passwordSalt;

        // Act
        string result = _userService.CreatePassword(password, out passwordSalt);

        // Assert
        Assert.IsNotNull(passwordSalt);
        Assert.IsNotEmpty(passwordSalt);
    }

    [Test]
    public void VerifyPasswordHash_ShouldReturnTrueForValidPassword()
    {
        // Arrange
        string password = "TestPassword";
        byte[] passwordSalt;
        string passwordHash = _userService.CreatePassword(password, out passwordSalt);

        // Act
        bool isVerified = _userService.VerifyPasswordHash(password, passwordHash, passwordSalt);

        // Assert
        Assert.IsTrue(isVerified);
    }

    [Test]
    public async Task RegisterAsync_ShouldThrowValidationException_WhenValidationFails()
    {
        // Arrange
        var registerDto = new RegisterDto { Email = "test.com", Password = "password" };
        var validationResult = new ValidationResult(new[] { new ValidationFailure("Email", "Invalid email") });

        _registerDtoValidatorMock.Setup(v => v.ValidateAsync(registerDto, default)).ReturnsAsync(validationResult);

        // Act & Assert
        var ex = Assert.ThrowsAsync<ValidationException>(() => _userService.RegisterAsync(registerDto));
        Assert.IsNotNull(ex);
        Assert.AreEqual(validationResult.Errors, ex.Errors);
    }

    [Test]
    public async Task Authenticate_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new AuthenticationRequest { Email = "test@example.com", Password = "password" };
        var user = new User
        {
            Password = _userService.CreatePassword("password", out byte[] salt),
            Salt = salt
        };
        _authenticationRequestValidatorMock.Setup(v => v.Validate(request)).Returns(new ValidationResult());
        _userRepositoryMock.Setup(u => u.GetUserByEmailAsync(request.Email)).ReturnsAsync(user);
        _jwtProviderMock.Setup(j => j.Generate(user)).Returns("token");

        // Act
        var token = await _userService.Authenticate(request);

        // Assert
        Assert.AreEqual("token", token);
    }

    [Test]
    public async Task Authenticate_ShouldThrowInvalidCredentialsException_WhenPasswordIsInvalid()
    {
        // Arrange
        var request = new AuthenticationRequest { Email = "test@example.com", Password = "wrongpassword" };
        var user = new User
        {
            Password = "hashedpassword",
            Salt = new byte[64] 
        };

        _authenticationRequestValidatorMock.Setup(v => v.Validate(request)).Returns(new ValidationResult());
        _userRepositoryMock.Setup(u => u.GetUserByEmailAsync(request.Email)).ReturnsAsync(user);
        _userService.CreatePassword("password", out byte[] salt);

        // Act & Assert
        var ex = Assert.ThrowsAsync<InvalidCredentialsException>(() => _userService.Authenticate(request));
        Assert.IsNotNull(ex);
    }

}
