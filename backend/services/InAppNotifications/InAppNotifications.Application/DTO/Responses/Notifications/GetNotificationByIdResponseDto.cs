namespace ch4rniauski.BankApp.InAppNotifications.Application.DTO.Responses.Notifications;

public sealed record GetNotificationByIdResponseDto(
    Guid Id,
    string Title,
    string Content,
    bool IsRead);
