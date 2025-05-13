using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiForm;

namespace AtonWebAPI.Controllers;

/// <summary>
/// API for User
/// </summary>

[ApiController]
[Route("api/[controller]/")]
public class UserController(IUserService userService) : ControllerBase
{
    
    /// <summary>
    /// Admin Only. (Пункт 1 в задании)
    /// </summary>
    /// <param name="userForm">Данные на основе которых создается новый пользователь.</param>
    /// <param name="creatorGuid">Guid админа, от которого происходит запрос.</param>
    
    [HttpPost("сreate")]
    public async Task<IActionResult> Create([FromBody] CreateUserForm userForm, [FromQuery] Guid creatorGuid)
    {
        await userService.CreateAsync(userForm, creatorGuid);
        return Ok();
    }
    
    /// <summary>
    /// Admin Only. (Пункт 5 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа, от которого происходит запрос.</param>
    
    [HttpGet("get-active-users")]
    public async Task<IActionResult> GetActiveUsers([FromQuery] Guid creatorGuid)
    {
        var returnObject = await userService.GetActiveUsersAsync(creatorGuid);
        return Ok(returnObject);
    }
    
    /// <summary>
    /// User only. (Пункт 7 в задании)
    /// </summary>
    /// <param name="password">Пароль пользователя.</param>
    /// <param name="login">Логин пользователя.</param>
    
    [HttpGet("get-user-by-login-password")]
    public async Task<IActionResult> GetUserByLoginAndPassword([FromQuery] string password, [FromQuery] string login)
    {
        var returnUser = await userService.GetUserByLoginAndPasswordAsync(login, password);
        return Ok(returnUser);
    }
    
    /// <summary>
    /// Admin Only. (Пункт 6 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа, от которого происходит запрос.</param>
    /// <param name="login">Login пользователя который нам необходим.</param>
    [HttpGet("get-user-by-login")]
    public async Task<IActionResult> GetUserByLogin([FromQuery] Guid creatorGuid, [FromQuery] string login)
    {
        var returnUser = await userService.GetUserByLoginAsync(creatorGuid, login);
        return Ok(returnUser);
    }

    /// <summary>
    /// Admin Only. (Пункт 8 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа, от которого происходит запрос.</param>
    /// <param name="dateTime">Дата в формате yyyy-mm-dd hh:mm:ss.</param>
    
    [HttpGet("get-users-older-than")]
    public async Task<IActionResult> GetUsersOlderThan([FromQuery] Guid creatorGuid, [FromQuery] DateTime dateTime)
    {
        var listUsers = await userService.GetUsersOlderThanAsync(dateTime, creatorGuid);
        return Ok(listUsers);
    }
    
    /// <summary>
    /// Admin or User. (Пункт 2 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа/пользователя, от которого происходит запрос.</param>
    /// <param name="userChangeGuid">Guid того, кому меняем данные.</param>
    /// <param name="dataForm">Поля, которые будут изменены. (Ненужные можно удалить.)</param>
    
    [HttpPatch("update-user-profile")]
    public async Task<IActionResult> UpdateUserProfile(
        [FromQuery] Guid creatorGuid,
        [FromQuery] Guid userChangeGuid,
        [FromBody] UpdateUserDataForm dataForm)
    {
        await userService.UpdateUserProfileAsync(creatorGuid, userChangeGuid, dataForm);
        return Ok();
    }
    
    /// <summary>
    /// Admin or User. (Пункт 4 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа/пользователя, от которого происходит запрос.</param>
    /// <param name="userChangeGuid">Guid того, кому меняем логин.</param>
    /// <param name="newLogin">Новый логин</param>
    
    [HttpPatch("change-login")]
    public async Task<IActionResult> ChangeLogin(
        [FromQuery] Guid creatorGuid,
        [FromQuery] Guid userChangeGuid,
        [FromQuery] string newLogin)
    {
        await userService.ChangeLoginAsync(creatorGuid, userChangeGuid, newLogin);
        return Ok();
    }
    /// <summary>
    /// Admin or User. (Пункт 3 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа/пользователя, от которого происходит запрос.</param>
    /// <param name="userChangeGuid">Guid того, кому мы меняем пароль.</param>
    /// <param name="newPassword"></param>
    
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromQuery] Guid creatorGuid,
        [FromQuery] Guid userChangeGuid,
        [FromQuery] string newPassword)
    {
        await userService.ChangePasswordAsync(creatorGuid, userChangeGuid, newPassword);
        return Ok();
    }
    
    /// <summary>
    /// Admin Only. (Пункт 10 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа, от которого происходит запрос.</param>
    /// <param name="recoverableGuid">Guid того, кого мы восстанавливаем.</param>
    
    [HttpPatch("recover")]
    public async Task<IActionResult> Recover(Guid creatorGuid, Guid recoverableGuid)
    {
        await userService.RecoverAsync(creatorGuid, recoverableGuid);
        return Ok();
    }
    
    /// <summary>
    /// Admin Only. (Пункт 9 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа, от которого происходит запрос.</param>
    /// <param name="removeUserLogin">Логин того, кого удаляем.</param>
    
    [HttpPatch("soft-delete")]
    public async Task<IActionResult> SoftDelete([FromQuery] Guid creatorGuid, [FromQuery] string removeUserLogin)
    {
        await userService.SoftDeleteAsync(creatorGuid, removeUserLogin);
        return Ok();
    }

    /// <summary>
    /// Admin Only. (Пункт 9 в задании)
    /// </summary>
    /// <param name="creatorGuid">Guid админа, от которого происходит запрос.</param>
    /// <param name="removeUserLogin">Логин того, кого удаляем.</param>
    
    [HttpDelete("hard-delete")]
    public async Task<IActionResult> HardDelete([FromQuery] Guid creatorGuid, [FromQuery] string removeUserLogin)
    {
        await userService.HardDeleteAsync(creatorGuid, removeUserLogin);
        return Ok();
    }
}