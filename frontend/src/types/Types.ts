/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

/** @format int32 */
export enum DomainEnumsTransactionType {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
}

/** @format int32 */
export enum DomainEnumsSavingStatus {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2,
  Value3 = 3,
}

/** @format int32 */
export enum DomainEnumsRole {
  Value0 = 0,
  Value1 = 1,
}

export interface ContractsDTOsBudgetBudgetDto {
  /** @format int32 */
  id?: number;
  /** @format int32 */
  categoryId?: number;
  /** @format double */
  amountLimit?: number;
  /** @format double */
  currentAmount?: number;
  /** @format int32 */
  month?: number;
  /** @format int32 */
  year?: number;
  /** @format date-time */
  createdAt?: string;
}

export interface ContractsDTOsBudgetCreateBudgetDto {
  /** @format int32 */
  categoryId?: number;
  /** @format double */
  amountLimit?: number;
  /** @format int32 */
  month?: number;
  /** @format int32 */
  year?: number;
}

export interface ContractsDTOsBudgetUpdateBudgetDto {
  /** @format double */
  amountLimit?: number | null;
  /** @format double */
  currentAmount?: number | null;
}

export interface ContractsDTOsCategoryCategoryDto {
  /** @format int32 */
  id?: number;
  name?: string | null;
  /** @format double */
  allTimeAmountSpent?: number;
  /** @format double */
  allTimeAmountEarned?: number;
  /** @format int32 */
  userId?: number;
  /** @format date-time */
  createdAt?: string;
}

export interface ContractsDTOsCategoryCreateCategoryDto {
  name: string | null;
  /** @format int32 */
  userId: number;
  /** @format double */
  allTimeAmountSpent?: number;
  /** @format double */
  allTimeAmountEarned?: number;
}

export interface ContractsDTOsCategoryUpdateCategoryDto {
  name?: string | null;
  /** @format double */
  allTimeAmountSpent?: number | null;
  /** @format double */
  allTimeAmountEarned?: number | null;
}

export interface ContractsDTOsCurrencyCreateCurrencyDto {
  code?: string | null;
  name?: string | null;
  symbol?: string | null;
}

export interface ContractsDTOsCurrencyCurrencyDto {
  /** @format int32 */
  id?: number;
  code?: string | null;
  name?: string | null;
  symbol?: string | null;
  /** @format date-time */
  createdAt?: string;
}

export interface ContractsDTOsCurrencyUpdateCurrencyDto {
  code?: string | null;
  name?: string | null;
  symbol?: string | null;
}

export interface ContractsDTOsPaymentMethodCreatePaymentMethodDto {
  name?: string | null;
  description?: string | null;
}

export interface ContractsDTOsPaymentMethodPaymentMethodDto {
  /** @format int32 */
  id?: number;
  name?: string | null;
  description?: string | null;
  /** @format date-time */
  createdAt?: string;
}

export interface ContractsDTOsPaymentMethodUpdatePaymentMethodDto {
  name?: string | null;
  description?: string | null;
}

export interface ContractsDTOsSavingCreateSavingDto {
  name?: string | null;
  description?: string | null;
  /** @format double */
  targetAmount?: number;
  /** @format date-time */
  targetDate?: string | null;
  userIds?: number[] | null;
}

export interface ContractsDTOsSavingSavingDto {
  /** @format int32 */
  id?: number;
  name?: string | null;
  code?: string | null;
  description?: string | null;
  /** @format double */
  targetAmount?: number;
  /** @format double */
  currentAmount?: number;
  /** @format double */
  remainingAmount?: number;
  /** @format date-time */
  createdAt?: string;
  /** @format date-time */
  targetDate?: string | null;
  status?: DomainEnumsSavingStatus;
  contributors?: ContractsDTOsUserSavingUserSavingDto[] | null;
}

export interface ContractsDTOsSavingUpdateSavingDto {
  name?: string | null;
  description?: string | null;
  /** @format double */
  targetAmount?: number | null;
  /** @format double */
  currentAmount?: number | null;
  /** @format date-time */
  targetDate?: string | null;
  status?: DomainEnumsSavingStatus;
  userIds?: number[] | null;
}

export interface ContractsDTOsTransactionCreateTransactionDto {
  /** @format int32 */
  userId?: number;
  /** @format int32 */
  categoryId?: number | null;
  /** @format int32 */
  paymentMethodId?: number;
  /** @format int32 */
  savingId?: number | null;
  /** @format double */
  amount?: number;
  type?: DomainEnumsTransactionType;
  description?: string | null;
  /** @format date-time */
  date?: string;
}

export interface ContractsDTOsTransactionTransactionDto {
  /** @format int32 */
  id?: number;
  /** @format int32 */
  userId?: number;
  /** @format int32 */
  categoryId?: number | null;
  /** @format int32 */
  paymentMethodId?: number;
  /** @format int32 */
  savingId?: number | null;
  /** @format double */
  amount?: number;
  type?: DomainEnumsTransactionType;
  description?: string | null;
  /** @format date-time */
  date?: string;
  /** @format date-time */
  createdAt?: string;
}

export interface ContractsDTOsUserLoginUserDto {
  email?: string | null;
  password?: string | null;
}

export interface ContractsDTOsUserRegisterUserDto {
  username?: string | null;
  email?: string | null;
  password?: string | null;
  role?: DomainEnumsRole;
  /** @format int32 */
  currencyId?: number;
}

export interface ContractsDTOsUserUpdateUserDto {
  username?: string | null;
  email?: string | null;
  /** @format int32 */
  currencyId?: number | null;
}

export interface ContractsDTOsUserUserDto {
  /** @format int32 */
  id?: number;
  username?: string | null;
  email?: string | null;
  /** @format date-time */
  createdAt?: string;
  /** @format int32 */
  currencyId?: number;
}

export interface ContractsDTOsUserSavingCreateUserSavingDto {
  /** @format int32 */
  userId?: number;
  /** @format int32 */
  savingId?: number;
}

export interface ContractsDTOsUserSavingUpdateUserSavingDto {
  /** @format double */
  contributedAmount?: number | null;
}

export interface ContractsDTOsUserSavingUserSavingDto {
  /** @format int32 */
  userId?: number;
  username?: string | null;
  /** @format int32 */
  savingId?: number;
  /** @format double */
  contributedAmount?: number;
  /** @format date-time */
  joinedAt?: string;
}

export interface MicrosoftAspNetCoreMvcProblemDetails {
  type?: string | null;
  title?: string | null;
  /** @format int32 */
  status?: number | null;
  detail?: string | null;
  instance?: string | null;
  [key: string]: any;
}

export interface SampleCkWebAppContractsDTOsBudgetBudgetSummaryDto {
  /** @format int32 */
  userId?: number;
  /** @format double */
  totalBudgetAmount?: number;
  /** @format double */
  totalSpentAmount?: number;
  /** @format double */
  totalRemainingAmount?: number;
  /** @format double */
  spentPercentage?: number;
  /** @format int32 */
  budgetCount?: number;
  /** @format int32 */
  month?: number;
  /** @format int32 */
  year?: number;
}

export interface SampleCkWebAppContractsDTOsCommonPaginatedResponse1ContractsDTOsTransactionTransactionDtoSampleCkWebAppContractsVersion1000CultureNeutralPublicKeyTokenNull {
  data?: ContractsDTOsTransactionTransactionDto[] | null;
  /** @format int32 */
  total?: number;
  /** @format int32 */
  page?: number;
  /** @format int32 */
  limit?: number;
  /** @format int32 */
  totalPages?: number;
}

export interface SampleCkWebAppContractsDTOsUserAuthResponseDto {
  token?: string | null;
  user?: ContractsDTOsUserUserDto;
}

export type QueryParamsType = Record<string | number, any>;
export type ResponseFormat = keyof Omit<Body, "body" | "bodyUsed">;

export interface FullRequestParams extends Omit<RequestInit, "body"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat;
  /** request body */
  body?: unknown;
  /** base url */
  baseUrl?: string;
  /** request cancellation token */
  cancelToken?: CancelToken;
}

export type RequestParams = Omit<
  FullRequestParams,
  "body" | "method" | "query" | "path"
>;

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string;
  baseApiParams?: Omit<RequestParams, "baseUrl" | "cancelToken" | "signal">;
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<RequestParams | void> | RequestParams | void;
  customFetch?: typeof fetch;
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown>
  extends Response {
  data: D;
  error: E;
}

type CancelToken = Symbol | string | number;

export enum ContentType {
  Json = "application/json",
  JsonApi = "application/vnd.api+json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = "";
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private abortControllers = new Map<CancelToken, AbortController>();
  private customFetch = (...fetchParams: Parameters<typeof fetch>) =>
    fetch(...fetchParams);

  private baseApiParams: RequestParams = {
    credentials: "same-origin",
    headers: {},
    redirect: "follow",
    referrerPolicy: "no-referrer",
  };

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig);
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key);
    return `${encodedKey}=${encodeURIComponent(typeof value === "number" ? value : `${value}`)}`;
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key]);
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key];
    return value.map((v: any) => this.encodeQueryParam(key, v)).join("&");
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {};
    const keys = Object.keys(query).filter(
      (key) => "undefined" !== typeof query[key],
    );
    return keys
      .map((key) =>
        Array.isArray(query[key])
          ? this.addArrayQueryParam(query, key)
          : this.addQueryParam(query, key),
      )
      .join("&");
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery);
    return queryString ? `?${queryString}` : "";
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string")
        ? JSON.stringify(input)
        : input,
    [ContentType.JsonApi]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string")
        ? JSON.stringify(input)
        : input,
    [ContentType.Text]: (input: any) =>
      input !== null && typeof input !== "string"
        ? JSON.stringify(input)
        : input,
    [ContentType.FormData]: (input: any) => {
      if (input instanceof FormData) {
        return input;
      }

      return Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key];
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === "object" && property !== null
              ? JSON.stringify(property)
              : `${property}`,
        );
        return formData;
      }, new FormData());
    },
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  };

  protected mergeRequestParams(
    params1: RequestParams,
    params2?: RequestParams,
  ): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected createAbortSignal = (
    cancelToken: CancelToken,
  ): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken);
      if (abortController) {
        return abortController.signal;
      }
      return void 0;
    }

    const abortController = new AbortController();
    this.abortControllers.set(cancelToken, abortController);
    return abortController.signal;
  };

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken);

    if (abortController) {
      abortController.abort();
      this.abortControllers.delete(cancelToken);
    }
  };

  public request = async <T = any, E = any>({
    body,
    secure,
    path,
    type,
    query,
    format,
    baseUrl,
    cancelToken,
    ...params
  }: FullRequestParams): Promise<HttpResponse<T, E>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const queryString = query && this.toQueryString(query);
    const payloadFormatter = this.contentFormatters[type || ContentType.Json];
    const responseFormat = format || requestParams.format;

    return this.customFetch(
      `${baseUrl || this.baseUrl || ""}${path}${queryString ? `?${queryString}` : ""}`,
      {
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type && type !== ContentType.FormData
            ? { "Content-Type": type }
            : {}),
        },
        signal:
          (cancelToken
            ? this.createAbortSignal(cancelToken)
            : requestParams.signal) || null,
        body:
          typeof body === "undefined" || body === null
            ? null
            : payloadFormatter(body),
      },
    ).then(async (response) => {
      const r = response as HttpResponse<T, E>;
      r.data = null as unknown as T;
      r.error = null as unknown as E;

      const responseToParse = responseFormat ? response.clone() : response;
      const data = !responseFormat
        ? r
        : await responseToParse[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data;
              } else {
                r.error = data;
              }
              return r;
            })
            .catch((e) => {
              r.error = e;
              return r;
            });

      if (cancelToken) {
        this.abortControllers.delete(cancelToken);
      }

      if (!response.ok) throw data;
      return data;
    });
  };
}

/**
 * @title Expense Tracker API
 * @version v1
 * @contact API Support <support@expensetracker.com>
 *
 * A RESTful API for managing expenses, users, and categories in an expense tracking application.
 */
export class Api<
  SecurityDataType extends unknown,
> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsList
     * @summary Retrieves all budgets from the system
     * @request GET:/api/Budgets
     * @secure
     */
    budgetsList: (params: RequestParams = {}) =>
      this.request<ContractsDTOsBudgetBudgetDto[], void>({
        path: `/api/Budgets`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsCreate
     * @summary Creates a new budget in the system
     * @request POST:/api/Budgets
     * @secure
     */
    budgetsCreate: (
      data: ContractsDTOsBudgetCreateBudgetDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsBudgetBudgetDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Budgets`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsDetail
     * @summary Retrieves a specific budget by its ID
     * @request GET:/api/Budgets/{id}
     * @secure
     */
    budgetsDetail: (id: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsBudgetBudgetDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Budgets/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsUpdate
     * @summary Updates an existing budget
     * @request PUT:/api/Budgets/{id}
     * @secure
     */
    budgetsUpdate: (
      id: number,
      data: ContractsDTOsBudgetUpdateBudgetDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsBudgetBudgetDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Budgets/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsDelete
     * @summary Deletes a budget from the system
     * @request DELETE:/api/Budgets/{id}
     * @secure
     */
    budgetsDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Budgets/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsUserDetail
     * @summary Retrieves all budgets for a specific user (through their categories)
     * @request GET:/api/Budgets/user/{userId}
     * @secure
     */
    budgetsUserDetail: (userId: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsBudgetBudgetDto[],
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Budgets/user/${userId}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsUserMonthYearDetail
     * @summary Retrieves all budgets for a specific user for a specific month
     * @request GET:/api/Budgets/user/{userId}/month/{month}/year/{year}
     * @secure
     */
    budgetsUserMonthYearDetail: (
      userId: number,
      month: number,
      year: number,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsBudgetBudgetDto[],
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Budgets/user/${userId}/month/${month}/year/${year}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsUserSummaryList
     * @summary Get budget summary for a user (total budgeted, spent, remaining, percentage)
     * @request GET:/api/Budgets/user/{userId}/summary
     * @secure
     */
    budgetsUserSummaryList: (userId: number, params: RequestParams = {}) =>
      this.request<
        SampleCkWebAppContractsDTOsBudgetBudgetSummaryDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Budgets/user/${userId}/summary`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Budgets
     * @name BudgetsUserMonthYearSummaryList
     * @summary Get budget summary for a user for a specific month
     * @request GET:/api/Budgets/user/{userId}/month/{month}/year/{year}/summary
     * @secure
     */
    budgetsUserMonthYearSummaryList: (
      userId: number,
      month: number,
      year: number,
      params: RequestParams = {},
    ) =>
      this.request<
        SampleCkWebAppContractsDTOsBudgetBudgetSummaryDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Budgets/user/${userId}/month/${month}/year/${year}/summary`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesList
     * @summary Retrieves all categories for the authenticated user
     * @request GET:/api/Categories
     * @secure
     */
    categoriesList: (params: RequestParams = {}) =>
      this.request<
        ContractsDTOsCategoryCategoryDto[],
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Categories`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesCreate
     * @summary Creates a new category in the system
     * @request POST:/api/Categories
     * @secure
     */
    categoriesCreate: (
      data: ContractsDTOsCategoryCreateCategoryDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsCategoryCategoryDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Categories`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesDetail
     * @summary Retrieves a specific category by its ID
     * @request GET:/api/Categories/{id}
     * @secure
     */
    categoriesDetail: (id: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsCategoryCategoryDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Categories/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesUpdate
     * @summary Updates an existing category
     * @request PUT:/api/Categories/{id}
     * @secure
     */
    categoriesUpdate: (
      id: number,
      data: ContractsDTOsCategoryUpdateCategoryDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsCategoryCategoryDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Categories/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Categories
     * @name CategoriesDelete
     * @summary Deletes a category from the system
     * @request DELETE:/api/Categories/{id}
     * @secure
     */
    categoriesDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Categories/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Currencies
     * @name CurrenciesList
     * @summary Retrieves all currencies from the system
     * @request GET:/api/Currencies
     * @secure
     */
    currenciesList: (params: RequestParams = {}) =>
      this.request<ContractsDTOsCurrencyCurrencyDto[], void>({
        path: `/api/Currencies`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Currencies
     * @name CurrenciesCreate
     * @summary Creates a new currency in the system
     * @request POST:/api/Currencies
     * @secure
     */
    currenciesCreate: (
      data: ContractsDTOsCurrencyCreateCurrencyDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsCurrencyCurrencyDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Currencies`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Currencies
     * @name CurrenciesDetail
     * @summary Retrieves a specific currency by its ID
     * @request GET:/api/Currencies/{id}
     * @secure
     */
    currenciesDetail: (id: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsCurrencyCurrencyDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Currencies/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Currencies
     * @name CurrenciesUpdate
     * @summary Updates an existing currency
     * @request PUT:/api/Currencies/{id}
     * @secure
     */
    currenciesUpdate: (
      id: number,
      data: ContractsDTOsCurrencyUpdateCurrencyDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsCurrencyCurrencyDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Currencies/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Currencies
     * @name CurrenciesDelete
     * @summary Deletes a currency from the system
     * @request DELETE:/api/Currencies/{id}
     * @secure
     */
    currenciesDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Currencies/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags PaymentMethods
     * @name PaymentMethodsList
     * @summary Retrieves all payment methods from the system
     * @request GET:/api/PaymentMethods
     * @secure
     */
    paymentMethodsList: (params: RequestParams = {}) =>
      this.request<ContractsDTOsPaymentMethodPaymentMethodDto[], void>({
        path: `/api/PaymentMethods`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PaymentMethods
     * @name PaymentMethodsCreate
     * @summary Creates a new payment method in the system
     * @request POST:/api/PaymentMethods
     * @secure
     */
    paymentMethodsCreate: (
      data: ContractsDTOsPaymentMethodCreatePaymentMethodDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsPaymentMethodPaymentMethodDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/PaymentMethods`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PaymentMethods
     * @name PaymentMethodsDetail
     * @summary Retrieves a specific payment method by its ID
     * @request GET:/api/PaymentMethods/{id}
     * @secure
     */
    paymentMethodsDetail: (id: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsPaymentMethodPaymentMethodDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/PaymentMethods/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PaymentMethods
     * @name PaymentMethodsUpdate
     * @summary Updates an existing payment method
     * @request PUT:/api/PaymentMethods/{id}
     * @secure
     */
    paymentMethodsUpdate: (
      id: number,
      data: ContractsDTOsPaymentMethodUpdatePaymentMethodDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsPaymentMethodPaymentMethodDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/PaymentMethods/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags PaymentMethods
     * @name PaymentMethodsDelete
     * @summary Deletes a payment method from the system
     * @request DELETE:/api/PaymentMethods/{id}
     * @secure
     */
    paymentMethodsDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/PaymentMethods/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Savings
     * @name SavingsList
     * @summary Retrieves all savings from the system
     * @request GET:/api/Savings
     * @secure
     */
    savingsList: (params: RequestParams = {}) =>
      this.request<ContractsDTOsSavingSavingDto[], void>({
        path: `/api/Savings`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Savings
     * @name SavingsCreate
     * @summary Creates a new saving in the system
     * @request POST:/api/Savings
     * @secure
     */
    savingsCreate: (
      data: ContractsDTOsSavingCreateSavingDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsSavingSavingDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Savings`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Savings
     * @name SavingsUserDetail
     * @summary Retrieves savings for a specific user
     * @request GET:/api/Savings/user/{userId}
     * @secure
     */
    savingsUserDetail: (userId: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsSavingSavingDto[],
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Savings/user/${userId}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Savings
     * @name SavingsDetail
     * @summary Retrieves a specific saving by its ID
     * @request GET:/api/Savings/{id}
     * @secure
     */
    savingsDetail: (id: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsSavingSavingDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Savings/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Savings
     * @name SavingsUpdate
     * @summary Updates an existing saving
     * @request PUT:/api/Savings/{id}
     * @secure
     */
    savingsUpdate: (
      id: number,
      data: ContractsDTOsSavingUpdateSavingDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsSavingSavingDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Savings/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Savings
     * @name SavingsDelete
     * @summary Deletes a saving from the system
     * @request DELETE:/api/Savings/{id}
     * @secure
     */
    savingsDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Savings/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Savings
     * @name SavingsUserNonCompletedList
     * @summary Retrieves non-completed (active) savings for a specific user
     * @request GET:/api/Savings/user/{userId}/non-completed
     * @secure
     */
    savingsUserNonCompletedList: (userId: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsSavingSavingDto[],
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Savings/user/${userId}/non-completed`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserPaginatedList
     * @summary Retrieves paginated transactions for a specific user with optional filters
     * @request GET:/api/Transactions/user/{userId}/paginated
     * @secure
     */
    transactionsUserPaginatedList: (
      userId: number,
      query?: {
        /**
         * Page number (default: 1)
         * @format int32
         * @default 1
         */
        page?: number;
        /**
         * Items per page (default: 10, max: 100)
         * @format int32
         * @default 10
         */
        limit?: number;
        /** Filter by transaction type (0=Income, 1=Expense, 2=Saving) */
        type?: DomainEnumsTransactionType;
        /**
         * Filter by category ID
         * @format int32
         */
        categoryId?: number;
        /**
         * Filter by saving goal ID
         * @format int32
         */
        savingId?: number;
        /**
         * Filter by start date (yyyy-MM-dd)
         * @format date-time
         */
        startDate?: string;
        /**
         * Filter by end date (yyyy-MM-dd)
         * @format date-time
         */
        endDate?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<
        SampleCkWebAppContractsDTOsCommonPaginatedResponse1ContractsDTOsTransactionTransactionDtoSampleCkWebAppContractsVersion1000CultureNeutralPublicKeyTokenNull,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Transactions/user/${userId}/paginated`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserAllList
     * @summary Retrieves all transactions for a specific user without pagination
     * @request GET:/api/Transactions/user/{userId}/all
     * @secure
     */
    transactionsUserAllList: (userId: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsTransactionTransactionDto[],
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Transactions/user/${userId}/all`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsDetail
     * @summary Retrieves a specific transaction by its ID
     * @request GET:/api/Transactions/{id}
     * @secure
     */
    transactionsDetail: (id: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsTransactionTransactionDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Transactions/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsDelete
     * @summary Deletes a transaction from the system
     * @request DELETE:/api/Transactions/{id}
     * @secure
     */
    transactionsDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Transactions/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserIncomeMonthlyList
     * @summary Retrieves the total monthly income for a specific user
     * @request GET:/api/Transactions/user/{userId}/income/monthly
     * @secure
     */
    transactionsUserIncomeMonthlyList: (
      userId: number,
      query: {
        /**
         * The month (1-12)
         * @format int32
         */
        month: number;
        /**
         * The year
         * @format int32
         */
        year: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<any, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Transactions/user/${userId}/income/monthly`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserIncomeRangeList
     * @summary Retrieves total income for a user within a date range
     * @request GET:/api/Transactions/user/{userId}/income/range
     * @secure
     */
    transactionsUserIncomeRangeList: (
      userId: number,
      query: {
        /**
         * Start date (yyyy-MM-dd)
         * @format date-time
         */
        startDate: string;
        /**
         * End date (yyyy-MM-dd)
         * @format date-time
         */
        endDate: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<any, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Transactions/user/${userId}/income/range`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserExpenseMonthlyList
     * @summary Retrieves the total monthly expenses for a specific user
     * @request GET:/api/Transactions/user/{userId}/expense/monthly
     * @secure
     */
    transactionsUserExpenseMonthlyList: (
      userId: number,
      query: {
        /**
         * The month (1-12)
         * @format int32
         */
        month: number;
        /**
         * The year
         * @format int32
         */
        year: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<any, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Transactions/user/${userId}/expense/monthly`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserExpenseRangeList
     * @summary Retrieves total expenses for a user within a date range
     * @request GET:/api/Transactions/user/{userId}/expense/range
     * @secure
     */
    transactionsUserExpenseRangeList: (
      userId: number,
      query: {
        /**
         * Start date (yyyy-MM-dd)
         * @format date-time
         */
        startDate: string;
        /**
         * End date (yyyy-MM-dd)
         * @format date-time
         */
        endDate: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<any, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Transactions/user/${userId}/expense/range`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserSavingsMonthlyList
     * @summary Retrieves the total monthly savings for a specific user
     * @request GET:/api/Transactions/user/{userId}/savings/monthly
     * @secure
     */
    transactionsUserSavingsMonthlyList: (
      userId: number,
      query: {
        /**
         * The month (1-12)
         * @format int32
         */
        month: number;
        /**
         * The year
         * @format int32
         */
        year: number;
      },
      params: RequestParams = {},
    ) =>
      this.request<any, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Transactions/user/${userId}/savings/monthly`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsUserSavingsRangeList
     * @summary Retrieves total savings for a user within a date range
     * @request GET:/api/Transactions/user/{userId}/savings/range
     * @secure
     */
    transactionsUserSavingsRangeList: (
      userId: number,
      query: {
        /**
         * Start date (yyyy-MM-dd)
         * @format date-time
         */
        startDate: string;
        /**
         * End date (yyyy-MM-dd)
         * @format date-time
         */
        endDate: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<any, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Transactions/user/${userId}/savings/range`,
        method: "GET",
        query: query,
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Transactions
     * @name TransactionsCreate
     * @summary Creates a new transaction in the system
     * @request POST:/api/Transactions
     * @secure
     */
    transactionsCreate: (
      data: ContractsDTOsTransactionCreateTransactionDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsTransactionTransactionDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Transactions`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name UsersList
     * @summary Retrieves all users from the system
     * @request GET:/api/Users
     * @secure
     */
    usersList: (params: RequestParams = {}) =>
      this.request<ContractsDTOsUserUserDto[], void>({
        path: `/api/Users`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name UsersMeList
     * @summary Retrieves the current authenticated user's information
     * @request GET:/api/Users/me
     * @secure
     */
    usersMeList: (params: RequestParams = {}) =>
      this.request<
        ContractsDTOsUserUserDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Users/me`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name UsersDetail
     * @summary Retrieves a specific user by their ID
     * @request GET:/api/Users/{id}
     * @secure
     */
    usersDetail: (id: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsUserUserDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Users/${id}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name UsersUpdate
     * @summary Updates an existing user
     * @request PUT:/api/Users/{id}
     * @secure
     */
    usersUpdate: (
      id: number,
      data: ContractsDTOsUserUpdateUserDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsUserUserDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Users/${id}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name UsersDelete
     * @summary Deletes a user from the system
     * @request DELETE:/api/Users/{id}
     * @secure
     */
    usersDelete: (id: number, params: RequestParams = {}) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/Users/${id}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name UsersRegisterCreate
     * @summary Registers a new user in the system
     * @request POST:/api/Users/register
     * @secure
     */
    usersRegisterCreate: (
      data: ContractsDTOsUserRegisterUserDto,
      params: RequestParams = {},
    ) =>
      this.request<
        SampleCkWebAppContractsDTOsUserAuthResponseDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Users/register`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Users
     * @name UsersLoginCreate
     * @summary Authenticates a user and returns a JWT token
     * @request POST:/api/Users/login
     * @secure
     */
    usersLoginCreate: (
      data: ContractsDTOsUserLoginUserDto,
      params: RequestParams = {},
    ) =>
      this.request<
        SampleCkWebAppContractsDTOsUserAuthResponseDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/Users/login`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UserSavings
     * @name UsersSavingsList
     * @summary Retrieves all savings associated with a specific user
     * @request GET:/api/users/{userId}/savings
     * @secure
     */
    usersSavingsList: (userId: number, params: RequestParams = {}) =>
      this.request<
        ContractsDTOsUserSavingUserSavingDto[],
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/users/${userId}/savings`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UserSavings
     * @name UsersSavingsCreate
     * @summary Associates a saving with a user (adds saving to user's account)
     * @request POST:/api/users/{userId}/savings
     * @secure
     */
    usersSavingsCreate: (
      userId: number,
      data: ContractsDTOsUserSavingCreateUserSavingDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsUserSavingUserSavingDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/users/${userId}/savings`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UserSavings
     * @name UsersSavingsDetail
     * @summary Retrieves a specific saving associated with a user
     * @request GET:/api/users/{userId}/savings/{savingId}
     * @secure
     */
    usersSavingsDetail: (
      userId: number,
      savingId: number,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsUserSavingUserSavingDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/users/${userId}/savings/${savingId}`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UserSavings
     * @name UsersSavingsUpdate
     * @summary Updates a saving association for a user
     * @request PUT:/api/users/{userId}/savings/{savingId}
     * @secure
     */
    usersSavingsUpdate: (
      userId: number,
      savingId: number,
      data: ContractsDTOsUserSavingUpdateUserSavingDto,
      params: RequestParams = {},
    ) =>
      this.request<
        ContractsDTOsUserSavingUserSavingDto,
        MicrosoftAspNetCoreMvcProblemDetails | void
      >({
        path: `/api/users/${userId}/savings/${savingId}`,
        method: "PUT",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags UserSavings
     * @name UsersSavingsDelete
     * @summary Removes a saving association from a user
     * @request DELETE:/api/users/{userId}/savings/{savingId}
     * @secure
     */
    usersSavingsDelete: (
      userId: number,
      savingId: number,
      params: RequestParams = {},
    ) =>
      this.request<void, MicrosoftAspNetCoreMvcProblemDetails | void>({
        path: `/api/users/${userId}/savings/${savingId}`,
        method: "DELETE",
        secure: true,
        ...params,
      }),
  };
  health = {
    /**
     * No description
     *
     * @tags Health
     * @name HealthList
     * @summary Returns the health status of the application
     * @request GET:/health
     * @secure
     */
    healthList: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/health`,
        method: "GET",
        secure: true,
        ...params,
      }),
  };
}
