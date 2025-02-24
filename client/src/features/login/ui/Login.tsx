import { Formik, Form, Field, ErrorMessage } from "formik";
import { Button } from "@headlessui/react";
import { useMutation } from "react-query";
import { LoginType } from "@/entities/types/authType";
import AuthService from "@/entities/api/authService";
import * as Yup from "yup";

const LoginSchema = Yup.object().shape({
  login: Yup.string().required("Логин обязателен").trim(),
  password: Yup.string().required("Пароль обязателен").trim(),
});

export const Login = () => {
  const handleLogin = async ({ login, password }:LoginType) => {
    return await AuthService.login({ login, password });
  };

  const { mutate } = useMutation({
    mutationFn: handleLogin,
  });

  return (
    <div className="flex items-center pt-40 p-4 bg-[var(--main-color)] justify-center pl-52">
      <div className="flex flex-col overflow-hidden rounded-lg shadow-xl md:flex-row w-full lg:max-w-[500px]">
        <div className="p-5 bg-[var(--main-color)] h-[420px] md:flex-1">
          <h3 className="my-4 text-2xl text-[var(--white)] font-semibold">Account Login</h3>
          <Formik
            initialValues={{ login: "", password: "" }}
            validationSchema={LoginSchema}
            onSubmit={(values, { setSubmitting }) => {
              mutate(values);
              setSubmitting(false);
            }}
          >
            {({ isSubmitting }) => (
              <Form className="flex flex-col space-y-7">
                <div className="flex flex-col space-y-2">
                  <label htmlFor="login" className="text-sm font-semibold text-[var(--text-login)]">
                    Login
                  </label>
                  <Field
                    type="text"
                    name="login"
                    placeholder="Введите Логин"
                    className="px-4 py-2 transition duration-300 bg-[var(--bg-input-color)] rounded text-[var(--white)]"
                  />
                  <ErrorMessage
                    name="login"
                    component="div"
                    className="text-red-500 text-sm"
                  />
                </div>

                <div className="flex flex-col space-y-3">
                  <div className="flex items-center justify-between">
                    <label
                      htmlFor="password"
                      className="text-sm font-semibold text-[var(--text-login)]"
                    >
                      Password
                    </label>
                  </div>
                  <Field
                    type="password"
                    name="password"
                    placeholder="Введите пароль"
                    className="px-4 py-2 transition duration-300 bg-[var(--bg-input-color)] text-[var(--white)] rounded"
                  />
                  <ErrorMessage
                    name="password"
                    component="div"
                    className="text-red-500 text-sm"
                  />
                </div>

                <Button
                  type="submit"
                  disabled={isSubmitting}
                  className="bg-green-700 p-3 font-bold rounded-lg shadow-xl text-[var(--white)]"
                >
                  Login
                </Button>
              </Form>
            )}
          </Formik>
        </div>
      </div>
    </div>
  );
};