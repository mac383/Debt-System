using BusinessLayer.classes;
using DataAccessLayer.models;
using DataAccessLayer.models.Company_models;
using DataAccessLayer.models.User_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using static System.Net.Mime.MediaTypeNames;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //Completed Testing
        [HttpGet("GetAll", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Users>>> GetAllUsers(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Users>? users = await cls_Users.GetUsersAsync(companyId);

                if (users == null)
                    users = new List<md_Users>();

                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("GetActive", Name = "GetActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Users>>> GetActiveUsers(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Users>? users = await cls_Users.GetActiveUsersAsync(companyId);

                if (users == null)
                    users = new List<md_Users>();

                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        
        //Completed Testing
        [HttpGet("GetInActive", Name = "GetInActiveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<md_Users>>> GetInActiveUsers(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                List<md_Users>? users = await cls_Users.GetInActiveUsersAsync(companyId);

                if (users == null)
                    users = new List<md_Users>();

                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("Find/{userId}/{companyId}", Name = "Find")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_User>> Find(int userId, int companyId)
        {
            if (userId <= 0)
                return BadRequest($"Invalid user ID {userId}.");


            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                var user = await cls_Users.GetUserByIdAsync(userId, companyId);

                if (user == null)
                    return NotFound($"No user found.");

                return Ok
                    (
                        new md_User
                        (
                            user.UserId, user.FullName, user.UserName, user.Phone1, user.Phone2, user.TelegramId, user.Permissions,
                            user.Image, Convert.ToBoolean(user.IsActive), user.ByUser, user.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("FintByAuth/{userName}/{password}", Name = "FindByAuth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_UserAuth>> FindByAuth(string userName, string password)
        {

            if (string.IsNullOrEmpty(userName))
                return BadRequest("Invalid User name.");

            if (string.IsNullOrEmpty(password))
                return BadRequest("Invalid Password.");

            try
            {
                var user = await cls_Users.GetUserByLogInInfoAsync(userName, password);

                if (user == null)
                    return NotFound($"No user found.");

                return Ok
                    (
                        new md_UserAuth
                        (
                            user.UserId, user.FullName, user.UserName, user.Password, user.Phone1, user.Phone2, user.TelegramId,
                            user.Permissions, user.Image, Convert.ToBoolean(user.IsActive), user.ByUser, user.CompanyId
                        )
                    );
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("Count", Name = "GetUsersCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetUsersCount(int companyId)
        {
            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Users.GetUsersCountAsync(companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("IsUserNameExist/{userName}/{companyId}", Name = "IsUserNameExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsUserNameExist(string userName, int companyId)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest("Invalid  user Name.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Users.IsUserNameExistAsync(userName, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("IsUserNameExist/{userId}/{userName}/{companyId}", Name = "IsUserNameExistWithOutCurrentUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> IsUserNameExistWithOutCurrentUser(int userId, string userName, int companyId)
        {
            if (userId <= 0)
                return BadRequest($"Invalid user ID {userId}.");

            if (string.IsNullOrEmpty(userName))
                return BadRequest($"Invalid User Name.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Users.IsUserNameExistWithOutCurrentUserAsync(userId, userName, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("SetUserAsActive/{userId}/{companyId}", Name = "SetUserAsActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetUserAsActive(int userId, int companyId)
        {
            if (userId <= 0)
                return BadRequest($"Invalid user ID {userId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Users.SetUserAsActiveAsync(userId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpGet("SetUserAsInActive/{userId}/{companyId}", Name = "SetUserAsInActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> SetUserAsInActive(int userId, int companyId)
        {
            if (userId <= 0)
                return BadRequest($"Invalid user ID {userId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");

            try
            {
                return Ok(await cls_Users.SetUserAsInActiveAsync(userId, companyId));
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //Completed Testing
        [HttpPost("New", Name = "NewUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_User>> NewUser(md_NewUser user)
        {
            try
            {
                cls_Users? userEntity = new cls_Users
                {
                    FullName = user.FullName,
                    Phone1 = user.Phone1,
                    Phone2 = user.Phone2,
                    TelegramId = user.TelegramId,
                    CompanyId = user.CompanyId,
                    ByUser = user.ByUser,
                    UserName = user.UserName,
                    Password = user.Password,
                    Permissions = user.Permissions,
                    Image = user.Image
                };

                if (!userEntity.ValidateUserObject())
                    return BadRequest(new { message = "Invalid user data.", status = false, user = user });

                if (await userEntity.SaveAsync())
                {
                    int insertedId = userEntity.UserId;
                    int companyId = userEntity.CompanyId;
                    userEntity = await cls_Users.GetUserByIdAsync(userEntity.UserId, userEntity.CompanyId);

                    if (userEntity == null)
                        return CreatedAtRoute
                        (
                            nameof(Find),
                            new
                            {
                                insertedId,
                                companyId
                            },
                            new
                            {
                                message = "Inserted new user successfully.",
                                status = true
                            }
                        );

                    md_User insertedUser = new md_User
                        (
                            userEntity.UserId, userEntity.FullName, userEntity.UserName,
                            userEntity.Phone1, userEntity.Phone2, userEntity.TelegramId,
                            userEntity.Permissions, userEntity.Image, Convert.ToBoolean(userEntity.IsActive),
                            userEntity.ByUser, userEntity.CompanyId
                        );

                    return CreatedAtRoute
                        (
                            nameof(Find),
                            new
                            {

                                insertedUser.UserId,
                                insertedUser.CompanyId
                            },
                            new
                            {
                                message = "Inserted new user successfully.",
                                status = true,
                                user = insertedUser
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to insert new user.", user = user });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        //Completed Testing
        [HttpPut("UpdateUser", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_UpdateUser>> UpdateUser(md_UpdateUser user)
        {
            if (user.UserId <= 0)
                return BadRequest($"Invalid user ID {user.UserId}.");

            if (user.CompanyId <= 0)
                return BadRequest($"Invalid company ID {user.CompanyId}.");

            try
            {
                cls_Users? userEntity = new cls_Users
                    (
                        user.UserId, user.FullName, user.Phone1, user.Phone2, user.TelegramId, user.CompanyId, user.ByUser,
                        user.UserName, user.Permissions, user.Image, user.IsActive
                    );

                if (!userEntity.ValidateUserObject())
                    return BadRequest(new { message = "Invalid user data.", status = false, user = user });

                if (await userEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated user successfully.",
                                status = true,
                                user = user
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update user." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        //Completed Testing
        [HttpPut("UpdateCurrentUser", Name = "UpdateCurrentUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<md_UpdateCurrentUser>> UpdateCurrentUser(md_UpdateCurrentUser user)
        {
            if (user.UserId <= 0)
                return BadRequest($"Invalid user ID {user.UserId}.");

            if (user.CompanyId <= 0)
                return BadRequest($"Invalid company ID {user.CompanyId}.");

            try
            {
                cls_Users? userEntity = new cls_Users
                    (
                        user.UserId, user.FullName, user.UserName, user.Password, user.Phone1, user.Phone2, user.TelegramId,
                        0, user.Image, user.IsActive, user.ByUser, user.CompanyId
                    );

                if (!userEntity.ValidateUserObject())
                    return BadRequest(new { message = "Invalid user data.", status = false, user = user });

                if (await userEntity.SaveAsync())
                {
                    return Ok
                        (
                            new
                            {
                                message = "Updated user successfully.",
                                status = true,
                                user = user
                            }
                        );
                }
                else
                    return BadRequest(new { status = false, message = "Failed to update user." });
            }
            catch
            {
                return StatusCode(500, new { status = false, message = "An error occurred while processing your request." });
            }
        }

        //Completed Testing
        [HttpDelete("Delete", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUser(int userId, int companyId, int byUser)
        {
            if (userId <= 0)
                return BadRequest($"Invalid user ID {userId}.");

            if (companyId <= 0)
                return BadRequest($"Invalid company ID {companyId}.");
            
            if (byUser <= 0)
                return BadRequest($"Invalid by user ID {byUser}.");

            try
            {
                if (await cls_Users.DeleteUser(userId, companyId, byUser))
                    return Ok(new { status = true, message = $"User with ID {userId} has been deleted successfully." });
                else
                    return NotFound(new { status = false, message = $"User with ID {userId} not found. no rows deleted." });
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}