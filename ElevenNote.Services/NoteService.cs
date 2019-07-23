using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            Category category = new Category();
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Where(m => m.CategoryId == model.CategoryId)
                        .Single();
                category = query;


                var entity =
                    new Note()
                    {
                        OwnerId = _userId,
                        Title = model.Title,
                        CategoryId = model.CategoryId,
                        Category = category,
                        Content = model.Content,
                        CreatedUtc = DateTimeOffset.Now
                    };

                ctx.Notes.Add(entity);
                var actual = ctx.SaveChanges();
                return actual == 1;
            }            
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.OwnerId == _userId)

                        .Select(e => new NoteListItem
                        {
                            NoteId = e.NoteId,
                            Title = e.Title,
                            IsStarred = e.IsStarred,
                            Category = e.Category,
                            CreatedUtc = e.CreatedUtc
                        }
                        );
                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        CategoryId = entity.Category.CategoryId,
                        CategoryName = entity.Category.CategoryName,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            Category category = new Category();

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);

                var query =
                    ctx
                        .Categories
                        .Where(m => m.CategoryId == model.CategoryId)
                        .Single();
                category = query;

                entity.Title = model.Title;
                entity.Category = category;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.IsStarred = model.IsStarred;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);
                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
