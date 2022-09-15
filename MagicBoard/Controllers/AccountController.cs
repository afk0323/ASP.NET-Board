using MagicBoard.DataContext;
using MagicBoard.Models;
using MagicBoard.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicBoard.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            // 메서드와 동일한 이름을 가진 view를 반환한다.
            return View();
        }

        [HttpPost]
        // Models의 LoginViewModel을 model로 정의하고 사용한다.
        public IActionResult Login(LoginViewModel model)
        {
            // 로그인 모델이 정의되어있는 LoginViewModel에 정의 해둔필수 입력 값: ID, 비밀번호
            if(ModelState.IsValid) // 위에 loginviewmodel에서 사용자가 필수 입력값을 모두 적었는가
            {
                // DB를 열고 닫겠다
                using (var db = new MagicBoardDbContext())
                {
                    // DB에 유저아이디와 유저비밀번호와 일치하는지 확인
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserPassword.Equals(model.UserPassword));

                    // 로그인에 성공했을 때
                    // 바로 윗줄에서 정의한 user의 DB의 같은 Model의 있는 사용자 번호를 USER_LOGIN_KEY에 int로 저장한다.
                    HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);
                    if (User != null)
                    {
                        //View의 Home폴더의 LoginSuccess로 바로간다.
                        return RedirectToAction("LoginSuccess", "Home");
                        
                    }
                }
                // 로그인에 실패했을 때
                ModelState.AddModelError(String.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");

            }
            // 메서드와 동일한 이름을 가진 view를 반환한다.
            return View(model);
        }

        public IActionResult Logout()
        {
            // HttpContext.Session.Clear(); 메모리가 부족할 때 관리자들이 사용하는 명령어
            // 로그아웃하면 세션의 USER_LOGIN_KEY를 제거한다.
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            
            // 로그아웃하면 View의 Home폴더의 Index페이지로 간다.
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 회원 가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            // 메서드와 동일한 이름을 가진 view를 반환한다.
            return View();
        }

        /// <summary>
        /// 회원 가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        // Models의 User을 model로 정의하고 사용한다.
        public IActionResult Register(User model)
        {
            // 로그인 모델이 정의되어있는 LoginViewModel에 정의 해둔필수 입력 값: ID, 비밀번호
            if (ModelState.IsValid)
            {
                // DB를 열고 닫겠다
                using (var db = new DataContext.MagicBoardDbContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges(); //commit
                }
                // 로그아웃하면 View의 Home폴더의 Index페이지로 간다.
                return RedirectToAction("Index", "Home");

            }
            // 메서드와 동일한 이름을 가진 view를 반환한다.
            return View();
        }
    }
}
