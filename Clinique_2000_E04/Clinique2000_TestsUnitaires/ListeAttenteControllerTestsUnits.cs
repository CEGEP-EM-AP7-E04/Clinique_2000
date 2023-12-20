using Clinique2000_Core.Models;
using Clinique2000_MVC.Controllers;
using Clinique2000_Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Clinique2000_TestsUnitaires
{
    public class ListeAttenteControllerTestsUnits
    {

        /// <summary>
        //Ce test v�rifie que laction Index du ListeAttenteController renvoie correctement une vue contenant une liste de ListeAttente. Le test utilise un mock de IListeAttenteService
        //pour retourner une liste pr�d�finie de ListeAttente. Il sassure que le r�sultat de laction Index est un ViewResult et que le mod�le de la vue correspond � la liste mock�e,
        //tant en type quen nombre d�l�ments. Ce test confirme que le contr�leur traite et affiche correctement les donn�es provenant du service.
        /// </summary>
        [Fact]
        public async Task Index_RetourneVueAvecListeAttente()
        {
            // Arrange
            var mockListeAttenteService = new Mock<IListeAttenteService>();
            var listeAttenteTest = new List<ListeAttente>
                    {
                        new ListeAttente
                        {
                            ListeAttenteID = 1,
                            IsOuverte = true,
                            DateEffectivite = new DateTime(2023, 1, 1),
                            HeureOuverture = new TimeSpan(9, 0, 0),
                            HeureFermeture = new TimeSpan(17, 0, 0),
                            NbMedecinsDispo = 5,
                            dureeConsultationMinutes = 30,
                            CliniqueID = 1
                        },
                        new ListeAttente
                        {
                            ListeAttenteID = 2,
                            IsOuverte = false,
                            DateEffectivite = new DateTime(2023, 1, 2),
                            HeureOuverture = new TimeSpan(8, 30, 0),
                            HeureFermeture = new TimeSpan(16, 30, 0),
                            NbMedecinsDispo = 4,
                            dureeConsultationMinutes = 45,
                            CliniqueID = 2
                        }
                    };


            mockListeAttenteService.Setup(s => s.ObtenirToutAsync()).ReturnsAsync(listeAttenteTest);

            var mockClinique2000Services = new Mock<IClinique2000Services>();
            mockClinique2000Services.Setup(s => s.listeAttente).Returns(mockListeAttenteService.Object);

            var controller = new ListeAttenteController(mockClinique2000Services.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<ListeAttente>>(viewResult.Model);
            Assert.Equal(listeAttenteTest.Count, model.Count);
        }








        /// <summary>
        ///Ce test unitaire v�rifie le comportement de l'action Index du ListeAttenteController lorsqu'il n'y a pas de donn�es disponibles.
        ///Il utilise un service mock� IClinique2000Services, qui � son tour utilise un IListeAttenteService mock� pour retourner une liste vide
        ///de ListeAttente. Le test s'assure que l'action Index renvoie un ViewResult avec un mod�le vide, ce qui est le comportement attendu dans
        ///le cas o� il n'y a pas de donn�es � afficher. Ce test est essentiel pour confirmer que le contr�leur g�re correctement les situations o�
        ///la source de donn�es est vide.
        /// 
        /// </summary>
      
        [Fact]
        public async Task Index_QuandAucuneDonnee_RetourneVueVide()
        {
            // Arrange
            var mockService = new Mock<IClinique2000Services>();
            var listeAttenteVide = new List<ListeAttente>(); // Liste vide pour simuler l'absence de donn�es

            var mockListeAttenteService = new Mock<IListeAttenteService>();
            mockListeAttenteService.Setup(s => s.ObtenirToutAsync()).ReturnsAsync(listeAttenteVide);

            mockService.Setup(s => s.listeAttente).Returns(mockListeAttenteService.Object);

            var controller = new ListeAttenteController(mockService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<ListeAttente>>(viewResult.Model);
            Assert.Empty(model); // V�rifier que le mod�le est vide
        }



        /// <summary>
        /// Teste que l'action Details retourne une vue avec le bon objet ListeAttente pour un ID sp�cifique
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Details_AvecIdValide_RetourneVueAvecListeAttenteSpecifique()
        {
            // Arrange
            var mockService = new Mock<IClinique2000Services>();
            var listeAttenteAttendue = new ListeAttente
            {
                ListeAttenteID = 1,
                IsOuverte = true,
                DateEffectivite = new DateTime(2023, 4, 10),
                HeureOuverture = new TimeSpan(9, 0, 0),
                HeureFermeture = new TimeSpan(17, 0, 0),
                NbMedecinsDispo = 3,
                dureeConsultationMinutes = 15,
                CliniqueID = 101,
            };

            mockService.Setup(s => s.listeAttente.ObtenirParIdAsync(1)).ReturnsAsync(listeAttenteAttendue);

            var controller = new ListeAttenteController(mockService.Object);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ListeAttente>(viewResult.Model);
            Assert.Equal(listeAttenteAttendue, model);
        }


        [Fact]
        public async Task Details_AvecIdInvalide_RetourneNotFound()
        {
            // Arrange
            var mockService = new Mock<IClinique2000Services>();
            var listeAttenteAttendue = new ListeAttente
            {
                ListeAttenteID = int.MaxValue,
                IsOuverte = true,
                DateEffectivite = new DateTime(2023, 4, 10),
                HeureOuverture = new TimeSpan(9, 0, 0),
                HeureFermeture = new TimeSpan(17, 0, 0),
                NbMedecinsDispo = 3,
                dureeConsultationMinutes = 15,
                CliniqueID = 101,
            };

            mockService.Setup(s => s.listeAttente.ObtenirParIdAsync(It.IsAny<int>())).ReturnsAsync((ListeAttente)null);

            var controller = new ListeAttenteController(mockService.Object);

            // Act
            var result = await controller.Details(999); // Utiliser un ID qui n'existe probablement pas

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}