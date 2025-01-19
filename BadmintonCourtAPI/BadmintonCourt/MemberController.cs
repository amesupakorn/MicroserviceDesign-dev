using Microsoft.AspNetCore.Mvc;
using BadmintonCourtAPI.Models;
[ApiController]
[Route("api/member")]
public class MemberController : ControllerBase
{
    private static List<Member> Members = new()
    {
        new Member { Id = 1, Name = "Supakorn", Points = 100 },
        new Member { Id = 2, Name = "Mananchaya", Points = 200 }
    };

    [HttpGet]
    public IActionResult GetAllMembers()
    {
        return Ok(Members);
    }

    [HttpGet("{id}")]
    public IActionResult GetMemberById(int id)
    {
        var member = Members.FirstOrDefault(m => m.Id == id);
        if (member == null)
            return NotFound("Member not found!");

        return Ok(member);
    }

    [HttpPost]
    public IActionResult AddMember([FromBody] Member newMember)
    {
        newMember.Id = Members.Count + 1;
        Members.Add(newMember);
        return Ok("Member added successfully!");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMember(int id, [FromBody] Member updatedMember)
    {
        var member = Members.FirstOrDefault(m => m.Id == id);
        if (member == null)
            return NotFound("Member not found!");

        member.Name = updatedMember.Name;
        member.Points = updatedMember.Points;
        return Ok("Member updated successfully!");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMember(int id)
    {
        var member = Members.FirstOrDefault(m => m.Id == id);
        if (member == null)
            return NotFound("Member not found!");

        Members.Remove(member);
        return Ok("Member deleted successfully!");
    }
}
