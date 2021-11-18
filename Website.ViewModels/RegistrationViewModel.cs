﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.ViewModels
{
    public class RegistrationViewModel : ConfirmMailViewModel
    {

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [RegularExpression("[a-zA-ZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя]", ErrorMessage = "Разрешен ввод только кириллицы/латиницы")]
        public string Name { get; set; }

        [Display(Name = "Фамилия", Prompt = "Фамилия")]
        [RegularExpression("[a-zA-ZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя]", ErrorMessage = "Разрешен ввод только кириллицы/латиницы")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        [RegularExpression("[a-zA-ZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя]", ErrorMessage = "Разрешен ввод только кириллицы/латиницы")]
        public string Patronymic { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string Birthday { get; set; }

        [Display(Name = "Страна")]
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public int CountryId { get; set; }


        public string MaxSelectedDateTime => DateTime.Now.AddYears(-18).ToString("yyyy-MM-dd");
    }
}