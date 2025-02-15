using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Enums.DemoApp.Exception
{
    public enum BaseEnumExceptionErrorMessages
    {
        // Default Handled Exceptions (Code : 1 => 100)
        UnknownException = 1,
        UnauthorizedException = 2,
        InvalidCredentialException = 3,
        ForbiddenException = 4,
        BussinessValidationException = 5,
        ParametersValidationException = 6,
        ProcessValidationException = 7,
        NotFoundException = 8,
        ExistingRecordException = 9,
        ArgumentNullException = 10,
        InvalidNullInputException = 11,
        BadParameterException = 12,
        DownloadException = 13,
        NoContentException = 14,
        ServiceUnavailableException = 15,
        UploadException = 16,
        SqlErrorException = 17,
        SmsException = 18,
        DecryptionException = 19,

        //Fluent Validation Exceptions (Code : 101 => 200)
        InputMustBeNotNullOrNotEmpty = 101,
        InputMustBeBetweenValues = 102,
        InputMustBeNullOrEmpty = 103,
        InputMustBeNumericValue = 104,
        InputMustBeLessThanExactValue = 105,
        InputMustBeLessThanOrEqualExactValue = 106,
        InputMustBeGreaterThanExactValue = 107,
        InputMustBeGreaterThanOrEqualExactValue = 108,
        InputMustBeHasExactLength = 109,
        InputMustBeHasMinLength = 110,
        InputMustBeHasMaxLength = 111,
        InputMustBeEqualExactValue = 112,
        InputMustBeNotEqualExactValue = 113,
        InputMustBeAfterTargetDate = 114,
        InputMustBeValidIsoDateFormat = 115,
        InputMustBeValidCreditCard = 116,
        InputMustBeValidEmail = 117,
        InputMustBeNotExistBefore = 118,
        InputValueMustBeExistsInEnum = 119,
        HaveAtLeastOneValue = 120,
        InputValuesHaveAtLeastOneInput = 121,
        InputListHaveAtLeastTwoRecordsRequired = 122,
        CompanySignatureAreInvalid = 123,
        PersonSignatureAreInvalid = 124,
        EmployeePersonCodeNotExists = 125,
        RepresentatorPersonCodeNotExists = 126,
        CompanyCodeNotExists = 127,
        ApplicantMustBeEitherEmployeeOrCompany = 128,

        // Authentication / UaePass / Access Token / Redis Server / General Services Exceptions (Code : 201 => 300)
        AuthenticationFailedException = 201,
        UaePassSop1UserException = 202,
        UAEPassGeneralMessageException = 203,
        AccessTokenNotValid = 204,
        RefreshTokenExpired = 205,
        RedisConnectionNotAvailable = 206,

        // App User Exceptions (Code : 301 => 800)
        AppUserNotExistInSystem = 301,
        AppUserExistInSystem = 302,
        AppUserMissingInformation = 303,
        AppuserInvalidCredentials = 304,
        AppuserDuplicatedInSystem = 305,
        CanNotUpsertAppUserEmirateAccess = 306,

        // Notifications Exceptions (Code : 801 => 1000)
        NotificationTemplateNotExist = 801,
        OTPGeneratedAndActive = 802,
        OTPIsInvalid = 803,
        OTPIsExpired = 804,

        // Multiple Dependents Validations error message (Code : 1000 => 1100)
        MultipleDepenedentsInputValidation = 1000,
        BothInputsAreMandatoryValidation = 1001,
        MultipleDepenedentsEmployeeSearchInputValidation = 1002,
        AllInputsAreMandatoryValidation = 1003,

        // Person Validation Exceptions (Code : 1000 => 1100)
        InvalidLabourEmployeeSearchCriteria = 1004,
        InvalidDomesticEmployeeSearchCriteria = 1005,
        InvalidDomesticSponsorSearchCriteria = 1006,
        InvalidPassportSearchCriteria = 1007,
        InvalidPersonalInformationSearchCriteria = 1008,
        InvalidICPSearchCriteria = 1009,

        LabourOfficeNotAllowed = 1101,
        DemoAlreadyRegisteredAgainstEmployer = 1102,
        DemoAlreadyRegisteredAgainstEmployee = 1103,
        DemoUnderCourtProcessForEmployee = 1104,
        DemoAmnestyExemption = 1105,
        UnableToRegisterLaborDemo = 1106,
        AnotherDemoExistsForSameParty = 1107,
        EmployeeNotExists = 1108,
        CompanyNotExists = 1109,
        SponsorNotExists = 1110,
        DomesticWorkerNotExists = 1111,
        ServiceCentredNotExists = 1112,
        UnableToRegisterDomesticWorkerDemo = 1113,
        UnableToRegisterDemo = 1114,
        UnifiedNumberRequired = 1115,
        InvalidIdentitySignature = 1116,
        ParseIdentitySignatureIsNull = 1117,
        RepresentatorNotExists = 1118,
        UnableToRegisterDemoDueToClaims = 1119,
        UnableToRegisterDemoDueToDemoParties = 2000,
        UnableToRegisterDemoDueToDemoContractDetails = 2001,
        UnableToRegisterDemoDetails = 2002,
        UnableToCreateDemoReferenceNumber = 2003,
        UnableToRegisterDemoHistory = 2004,
        InvalidDemoPartiesCount = 2005,
        InvalidDemoClaimsCount = 2006,
        InvalidDemoType = 2007,
        InvalidDemoPartyType = 2008,
        FileUploadLinkExpired = 2009,
        UnableToUploadFile = 2010,
        DemoReferenceIsRequired = 2011,
        RecordNotExistsForFileUrlGuid = 2012,
        AlreadyCaseInCourtEmployee = 2013,
        AlreadyFinalDecisionEmployee = 2014,
        DemoAlreadyClosedAgainstEmployer = 2015,
        DemoUnderProcessByOtherUser = 2016,
        ProvideUAENationalDetails = 2017,
        AlreadyDemoInDraft = 2018,
        AlreadyRegisteredUAEPublic = 2019,
        AlreadyCaseInCourtForPerson = 2020,
        InitiatorAndRespondentMobileNumberCannotBeSame = 2021,
        InvalidComplaintPartiesCount = 2022,
        InvalidComplaintClaimsCount= 2023

    }
}
