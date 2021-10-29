﻿using Database.DAL.Entities;
using Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private IRepository<User> _Users { get; }
        private IRepository<Post> _Posts { get; }

        public ProfileService(IRepository<User> users, IRepository<Post> posts)
        {
            _Users = users;
            _Posts = posts;
        }

        #region GetUserPostsWithFilter
        /// <summary>
        /// Получить посты указанного пользователя, у которых заголовок или текст содержит текст фильтра
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filterText"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetUserPostsWithFilter(int id, string filterText) => _Posts.Items.Where(post =>
            post.OwnerId.Equals(id) &&
            (post.Title != null && post.Title.Contains(filterText) ||
             post.Description != null && post.Description.Contains(filterText)))
            .OrderByDescending(post => post.CreatedDateTime);
        #endregion
            
        #region GetUserPosts

        /// <summary>
        /// Получить посты указанного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetUserPosts(int id) => _Posts.Items
            .Where(post => post.OwnerId.Equals(id))
            .OrderByDescending(post => post.CreatedDateTime);
        #endregion

        #region GetUserAsync
        /// <summary>
        /// Получить указанного пользователя асинхронно 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(int id) => await _Users.GetAsync(id);
        #endregion

        #region UploadPost
        /// <summary>
        /// Добавление поста
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public Post UploadPost(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title) && string.IsNullOrWhiteSpace(post.Description))
                return null;
            post.Owner = _Users.Get(post.OwnerId);
            return _Posts.Add(post);
        }
        #region Edit post
        /// <summary>
        /// Редактирование поста
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public Post EditPost(Post post)
        {
            var postDefault = _Posts.Get(post.Id);

            if (postDefault.OwnerId != post.OwnerId)
                return postDefault;

            postDefault.Title = post.Title;
            postDefault.Description = post.Description;

            _Posts.Update(postDefault);

            return postDefault;
        } 
        #endregion

        #endregion

        #region DeletePost
        /// <summary>
        /// Удаление поста по указанному id поста и id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemovePost(int idPost, int idUser)
        {
            var userPostExist = _Posts.Items.Any(post=>post.Id==idPost&&post.OwnerId==idUser);
            if(userPostExist)
                _Posts.Remove(idPost);
            return userPostExist;
        }
        #endregion
    }
}
