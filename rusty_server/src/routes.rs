use rocket::{Route, routes};
use rocket::Data;
use rocket::request::Form;
use rocket::Response;
// use rocket_contrib::json::{JsonError, Json};
use rocket::http::uri::Origin as URI;
use rocket::http::Method;


pub fn set_up() -> Vec<Route> {
    routes![
        get_test,
        post_test
    ]
}

#[get("/test")]
fn get_test<'r>() -> Response<'r> {
    let mut resp = Response::new()
    "It is working af".to_string()
}

#[post("/test", data="<nice>")]
fn post_test<'r>(nice: Form<String>) -> Response<'r> {
    Ok(format!("It is working more than af, you have send me this shiet: '{}'", nice.into_inner()))
}
