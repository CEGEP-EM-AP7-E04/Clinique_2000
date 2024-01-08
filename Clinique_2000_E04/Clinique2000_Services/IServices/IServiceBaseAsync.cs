﻿namespace Clinique2000_Services.IServices
{
    public interface IServiceBaseAsync<T> where T : class
    {
        Task<T> CreerAsync(T entity);
        Task SupprimerAsync(int id);
        Task<IReadOnlyList<T>> ObtenirToutAsync();
        Task<T?> ObtenirParIdAsync(int? id);
        Task<T?> ObtenirParNomAsync(string nom);
        Task EditerAsync(T entity);
    }
}
