using CM.Data;
using CM.DTOs;
using CM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class GetPageCountForIngredientsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrect_CountOfPagesForTheListing_DependOfIngredientsPerPage()
        {
            var options = TestUtils.GetOptions
                (nameof(ReturnCorrect_CountOfPagesForTheListing_DependOfIngredientsPerPage));

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "1",
                        Name = "Cocktail"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "target1",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "1",
                                Name = "vodka",
                                IngredientId = "1",
                                CocktailId = "1"
                            }
                        }
                    });
                //2
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "2",
                        Name = "Cocktail2"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "2",
                        Name = "target2",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "2",
                                Name = "vodka",
                                IngredientId = "2",
                                CocktailId = "2"
                            }
                        }
                    });
                //3
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "3",
                       Name = "Cocktail3"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "3",
                        Name = "target3",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "3",
                                Name = "vodka",
                                IngredientId = "3",
                                CocktailId = "3"
                            }
                        }
                    });
                //4
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "4",
                       Name = "Cocktail4"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "4",
                        Name = "target4",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "4",
                                Name = "vodka",
                                IngredientId = "4",
                                CocktailId = "4"
                            }
                        }
                    });
                //5
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "5",
                       Name = "Cocktail5"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "5",
                        Name = "target5",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "5",
                                Name = "vodka",
                                IngredientId = "5",
                                CocktailId = "5"
                            }
                        }
                    });
                //6
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "6",
                       Name = "Cocktail6"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "6",
                        Name = "target6",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "6",
                                Name = "vodka",
                                IngredientId = "6",
                                CocktailId = "6"
                            }
                        }
                    });
                //7
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "7",
                       Name = "Cocktail7"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "7",
                        Name = "target7",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "7",
                                Name = "vodka",
                                IngredientId = "7",
                                CocktailId = "7"
                            }
                        }
                    });
                //8
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "8",
                       Name = "Cocktail8"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "8",
                        Name = "target8",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "8",
                                Name = "vodka",
                                IngredientId = "8",
                                CocktailId = "8"
                            }
                        }
                    });
                //9
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "9",
                       Name = "Cocktail9"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "9",
                        Name = "target9",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "9",
                                Name = "vodka",
                                IngredientId = "9",
                                CocktailId = "9"
                            }
                        }
                    });
                //10
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "10",
                       Name = "Cocktail10"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "10",
                        Name = "target10",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "10",
                                Name = "vodka",
                                IngredientId = "10",
                                CocktailId = "10"
                            }
                        }
                    });
                //11
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "11",
                       Name = "Cocktail11"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "11",
                        Name = "target11",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "11",
                                Name = "vodka",
                                IngredientId = "11",
                                CocktailId = "11"
                            }
                        }
                    });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetPageCountForIngredientsAsync(10);
               
                Assert.AreEqual(2, result);
            }
        }

        [TestMethod]
        public async Task ReturnCorrect_CountOfPagesForTheListing_DependOfIngredientsPerPage_DontCountDeletedIngredients()
        {
            var options = TestUtils.GetOptions
                (nameof(ReturnCorrect_CountOfPagesForTheListing_DependOfIngredientsPerPage_DontCountDeletedIngredients));

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "1",
                        Name = "Cocktail"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "target1",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "1",
                                Name = "vodka",
                                IngredientId = "1",
                                CocktailId = "1"
                            }
                        }
                    });
                //2
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "2",
                        Name = "Cocktail2"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "2",
                        Name = "target2",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "2",
                                Name = "vodka",
                                IngredientId = "2",
                                CocktailId = "2"
                            }
                        }
                    });
                //3
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "3",
                       Name = "Cocktail3"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "3",
                        Name = "target3",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "3",
                                Name = "vodka",
                                IngredientId = "3",
                                CocktailId = "3"
                            }
                        }
                    });
                //4
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "4",
                       Name = "Cocktail4"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "4",
                        Name = "target4",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "4",
                                Name = "vodka",
                                IngredientId = "4",
                                CocktailId = "4"
                            }
                        }
                    });
                //5
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "5",
                       Name = "Cocktail5"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "5",
                        Name = "target5",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "5",
                                Name = "vodka",
                                IngredientId = "5",
                                CocktailId = "5"
                            }
                        }
                    });
                //6
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "6",
                       Name = "Cocktail6"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "6",
                        Name = "target6",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "6",
                                Name = "vodka",
                                IngredientId = "6",
                                CocktailId = "6"
                            }
                        }
                    });
                //7
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "7",
                       Name = "Cocktail7"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "7",
                        Name = "target7",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "7",
                                Name = "vodka",
                                IngredientId = "7",
                                CocktailId = "7"
                            }
                        }
                    });
                //8
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "8",
                       Name = "Cocktail8"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "8",
                        Name = "target8",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "8",
                                Name = "vodka",
                                IngredientId = "8",
                                CocktailId = "8"
                            }
                        }
                    });
                //9
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "9",
                       Name = "Cocktail9"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "9",
                        Name = "target9",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "9",
                                Name = "vodka",
                                IngredientId = "9",
                                CocktailId = "9"
                            }
                        }
                    });
                //10
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "10",
                       Name = "Cocktail10"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "10",
                        Name = "target10",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "10",
                                Name = "vodka",
                                IngredientId = "10",
                                CocktailId = "10"
                            }
                        }
                    });
                //11
                arrangeContext.Cocktails.Add(
                   new Cocktail
                   {
                       Id = "11",
                       Name = "Cocktail11"
                   });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "11",
                        Name = "target11",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "11",
                                Name = "vodka",
                                IngredientId = "11",
                                CocktailId = "11"
                            }
                        },
                        DateDeleted = DateTime.Now
                    });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetPageCountForIngredientsAsync(10);

                Assert.AreEqual(1, result);
            }
        }
    }
}
