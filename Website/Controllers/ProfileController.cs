﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Website.Controllers.Rules;
using Website.Infrastructure.Extensions;
using Website.ViewModels;
using Website.ViewModels.Friends;
using Website.ViewModels.Profile;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class ProfileController : Controller
    {
        private readonly IFileService FileService;
        private readonly IProfileService ProfileService;
        private readonly IPostingService PostingService;
        private readonly IFriendsService FriendsService;

        #region Get connected user id
        /// <summary>
        /// Получить подключенного пользователя с помощью claims
        /// </summary>
        private int GetConnectedUserID => int.Parse(User.GetConnectedUserId()); 
        #endregion

        public ProfileController(
            IProfileService profileService,
            IPostingService postingService, 
            IFileService fileService,
            IFriendsService friendsService)
        {
            ProfileService = profileService;
            PostingService = postingService;
            FileService = fileService;
            FriendsService = friendsService;
        }

        #region Profile
        /// <summary>
        /// Получение страницы профиля, с возмоджным указанием id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/profile")]
        public async Task<IActionResult> Profile(int id)
        {
            id = id == 0 ? GetConnectedUserID : id;
            var user = await ProfileService.GetUserAsync(id);
            if (user == null) return RedirectToAction("Profile");

            FriendViewModel FriendViewModel = null;

            if (id != GetConnectedUserID && id != 0)
                FriendViewModel = FriendsService.GetFriendViewModel(GetConnectedUserID, id);
            return View(new ProfileViewModel
            {
                User = user,
                IsOwner = id == GetConnectedUserID,
                UserPosts = ProfileService.GetUserPosts(id),
                FriendViewModel = FriendViewModel
            });
        } 
        #endregion
        
        #region Upload profile avatar
        [HttpPost]
        public ActionResult UploadAvatar(IFormFile image)
         {
            if (image != null)
            {
                FileService.UploadAvatar(image, HttpContext.GetConnectedUserId());
            }
            return RedirectToAction("Profile");
        } 
        #endregion
        
        #region Remove profile avatar
        [HttpPost]
        public ActionResult RemoveAvatar()
        {
            FileService.RemoveAvatar(HttpContext.GetConnectedUserId());
            return RedirectToAction("Profile");
        } 
        #endregion
        
        #region Upload profile post
        /// <summary>
        /// Добавление поста на страницу
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadPost(PostViewModel post) => PartialView("PostView", ProfileService.UploadPost(new(){
            Title=post.Title,
            Description=post.Description,
            OwnerId = GetConnectedUserID
            })); 
        #endregion

        #region Edit profile post
        /// <summary>
        /// Редактирование поста
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPost(PostViewModel post) => View("PostView", ProfileService.EditPost(new(){
            Id = post.Id,
            Title=post.Title,
            Description=post.Description,
            OwnerId = GetConnectedUserID
        })); 
        #endregion

        #region Delete profile post
        /// <summary>
        /// Удалить указанный пост с профиля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeletePost(int id) => ProfileService.RemovePost(id, GetConnectedUserID);

        #endregion

        #region Search user posts
        /// <summary>
        /// Поиск постов пользователя с фильтром
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filterText"></param>
        /// <returns></returns>
        public ActionResult SearchUserPosts(string filterText, int id)
        {
            id = id== 0 ? GetConnectedUserID : id;
            return View("PostsView",
                string.IsNullOrWhiteSpace(filterText)
                    ? ProfileService.GetUserPosts(id)
                    : ProfileService.GetUserPostsWithFilter(id, filterText));
        }
        #endregion

        #region SendReportForPost
        /// <summary>
        /// Append targeted post to report list
        /// </summary>
        /// <param name="postId"></param>
        public void SendReportForPost(int postId, int reportTypeId, string msg)
        {
            PostingService.SendReportToPost(postId, GetConnectedUserID, reportTypeId, msg);
        }
        #endregion

        public IActionResult SendFriendRequest(int targetUserId)
        {
            FriendsService.TrySendFriendshipRequest(GetConnectedUserID, targetUserId);
            return Redirect("/profile?id="+targetUserId);
        } 
        public IActionResult RemoveFriend(int id, int targetUserId)
        {
            FriendsService.TryRemoveUserFriendship(GetConnectedUserID, targetUserId);
            return Redirect("/profile?id="+targetUserId);
        }
        
    }
}
