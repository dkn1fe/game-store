import { useFormik, FormikValues } from "formik";
import { Button, Input } from "@headlessui/react";
import { useMutation } from "react-query";
import AuthService from "@/entities/api/authService";
import * as Yup from "yup";

export const Login = () => {
  const handleLogin = async ({
    login,
    password,
  }: {
    login: string;
    password: string;
  }) => await AuthService.login({ login, password });

  const { mutate } = useMutation({
    mutationFn: handleLogin,
    onSuccess: (data) => {
      console.log(data);
    },
  });

  const formik = useFormik({
    initialValues: {
      login: "",
      password: "",
    },
    validationSchema: Yup.object().shape({
      login: Yup.string().required("Login is required").trim(),
      password: Yup.string().required("Password is required").trim(),
    }),
    onSubmit: (values: FormikValues) => {
      const { login, password } = values;
      console.log(values);
      mutate({ login, password });
    },
  });

  return (
    <div className="flex items-center pt-40 p-4 bg-[var(--main-color)] justify-center pl-52">
      <div className="flex flex-col overflow-hidden rounded-lg shadow-xl md:flex-row w-full lg:max-w-[500px]">
        <div className="p-5 bg-[var(--main-color)] h-[420px] md:flex-1">
          <h3 className="my-4 text-2xl text-[var(--white)] font-semibold">
            Account Login
          </h3>
          <form
            onSubmit={formik.handleSubmit}
            className="flex flex-col space-y-7"
          >
            <div className="flex flex-col space-y-2">
              <label
                htmlFor="login"
                className="text-sm font-semibold text-[var(--text-login)]"
              >
                Login
              </label>
              <Input
                name="login"
                type="login"
                id="login"
                placeholder="Введите Логин"
                onBlur={formik.handleBlur}
                onChange={formik.handleChange}
                value={formik.values.login}
                autoFocus
                className="px-4 py-2 transition duration-300 bg-[var(--bg-input-color)] rounded text-[var(--white)]"
              />
              {formik.touched.login && formik.errors.login && (
                <div className="text-sm text-red-500 pt-1">
                  {formik.errors.login as string}
                </div>
              )}
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
              <Input
                type="password"
                id="password"
                name="password"
                placeholder="Введите пароль"
                onBlur={formik.handleBlur}
                onChange={formik.handleChange}
                value={formik.values.password}
                className="px-4 py-2 transition duration-300 bg-[var(--bg-input-color)] text-[var(--white)] rounded"
              />
              {formik.touched.password && formik.errors.password && (
                <div className="text-sm text-red-500">
                  {formik.errors.password as string}
                </div>
              )}
            </div>
            <Button
              type="submit"
              className="bg-green-700 p-3 font-bold rounded-lg shadow-xl text-[var(--white)]"
            >
              Login
            </Button>
          </form>
        </div>
      </div>
    </div>
  );
};
