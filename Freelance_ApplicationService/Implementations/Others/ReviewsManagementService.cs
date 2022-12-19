using Base.ManagementService;
using Freelance_ApplicationService.DTOs.Others;
using Freelance_Data.Entities.Others;
using Freelance_Repository.Implementations.EntityRepositories.Others;
using Freelance_Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.Implementations.Others
{
    public class ReviewsManagementService : IBaseManagementService
    {
        public static async Task<List<ReviewDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                ReviewsRepository ReviewsRepo = new ReviewsRepository(unitOfWork);
                List<Review> Reviews = await ReviewsRepo.GetAll();

                List<ReviewDTO> ReviewsDTO = new List<ReviewDTO>();

                if (Reviews != null)
                {
                    foreach (var item in Reviews)
                    {
                        ReviewsDTO.Add(new ReviewDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            ReviewedUserId = item.ReviewedUserId,
                            DateOfPosting = item.DateOfPosting,
                            DeliveredOnBudget = item.DeliveredOnBudget,
                            DeliveredOnTime = item.DeliveredOnTime,
                            Rating = item.Rating,
                            Comment = item.Comment,
                            TaskId = item.TaskId,
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return ReviewsDTO;
            }
        }

        public static async Task<ReviewDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                ReviewsRepository ReviewsRepo = new ReviewsRepository(unitOfWork);
                ReviewDTO ReviewDTO = new ReviewDTO();

                Review Review = await ReviewsRepo.GetById(id);

                if (Review != null)
                {
                    ReviewDTO.Id = Review.Id;
                    ReviewDTO.UserId = Review.UserId;
                    ReviewDTO.ReviewedUserId = Review.ReviewedUserId;
                    ReviewDTO.DateOfPosting = Review.DateOfPosting;
                    ReviewDTO.DeliveredOnBudget = Review.DeliveredOnBudget;
                    ReviewDTO.DeliveredOnTime = Review.DeliveredOnTime;
                    ReviewDTO.Rating = Review.Rating;
                    ReviewDTO.Comment = Review.Comment;
                    ReviewDTO.TaskId = Review.TaskId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return ReviewDTO;
            }
        }

        public static async Task Save(ReviewDTO ReviewDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                ReviewsRepository ReviewsRepo = new ReviewsRepository(unitOfWork);
                Review Review = new Review();

                if (ReviewDTO != null)
                {
                    if (ReviewDTO.Id == 0)
                    {
                        Review = new Review
                        {
                            UserId = ReviewDTO.UserId,
                            ReviewedUserId = ReviewDTO.ReviewedUserId,
                            DateOfPosting = ReviewDTO.DateOfPosting,
                            DeliveredOnBudget = ReviewDTO.DeliveredOnBudget,
                            DeliveredOnTime = ReviewDTO.DeliveredOnTime,
                            Rating = ReviewDTO.Rating,
                            Comment = ReviewDTO.Comment,
                            TaskId = ReviewDTO.TaskId
                        };
                    }
                    else
                    {
                        Review = new Review
                        {
                            Id = ReviewDTO.Id,
                            UserId = ReviewDTO.UserId,
                            ReviewedUserId = ReviewDTO.ReviewedUserId,
                            DateOfPosting = ReviewDTO.DateOfPosting,
                            DeliveredOnBudget = ReviewDTO.DeliveredOnBudget,
                            DeliveredOnTime = ReviewDTO.DeliveredOnTime,
                            Rating = ReviewDTO.Rating,
                            Comment = ReviewDTO.Comment,
                            TaskId = ReviewDTO.TaskId
                        };
                    }

                    await ReviewsRepo.Save(Review);
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }

        public static async Task Delete(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                ReviewsRepository ReviewsRepo = new ReviewsRepository(unitOfWork);
                Review Review = await ReviewsRepo.GetById(id);

                if (Review != null)
                {
                    await ReviewsRepo.Delete(Review);
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }
    }
}
